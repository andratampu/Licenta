import pyodbc

conn = pyodbc.connect('Driver={SQL Server};'
                      'Server=DESKTOP-3USCAD1;'
                      'Database=Licenta;'
                      'Trusted_Connection=yes;')

cursor = conn.cursor()

query = "SELECT [UserId],[RecipeId],[Rating] FROM [Licenta].[dbo].[Favorites]"

cursor.execute("Delete from Recommendations")
conn.commit()

import pandas as pd
from surprise import Dataset
from surprise import Reader

db_data = pd.read_sql(query,conn)
reader = Reader(rating_scale=(1,5))

ratings = Dataset.load_from_df(db_data[["UserId", "RecipeId", "Rating"]], reader)

import math

def distance(u1,u2,d):
    ssum = 0
    for r in d[u1]:
        if r in d[u2]:
            ssum += pow( d[u1][r] - d[u2][r] , 2)
    if sum == 0:
        return 0
    return math.sqrt(ssum)


def most_near(u,d, n = 10):
    scores = {}
    for o in d:
        if o != u:
            dist = distance(u,o,d)
            if dist == 0:
                continue
            scores[o] = dist
    dtc= dict(sorted( scores.items(), key=lambda item: item[1] ))
    return dict(list(dtc.items())[0: n])


def get_recommendations(u,d):
    similar = most_near(u,d, 10)
    recommendations = []
    common ={}
    for o in d:
        if o!=u:
            if o in similar.keys():
                for r in d[o]:
                    if r not in d[u]:
                        recommendations.append(r)
    return set(recommendations)

d = {}
for i in db_data['UserId'].unique():
    d[i] = {db_data['RecipeId'][j]: db_data['Rating'][j] for j in db_data[db_data['UserId']==i].index}


for user in d:
    recommendations = get_recommendations(user,d)
    command ='Insert into Recommendations (UserId,Recommendations) values (\'' +user+'\',\'' +  ','.join(str(x) for x in recommendations) + '\')'
    cursor.execute(command)
    conn.commit()
