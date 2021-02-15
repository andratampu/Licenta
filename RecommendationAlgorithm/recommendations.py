import pyodbc

conn = pyodbc.connect('Driver={SQL Server};'
                      'Server=DESKTOP-3USCAD1;'
                      'Database=Licenta;'
                      'Trusted_Connection=yes;')

cursor = conn.cursor()

query = "SELECT [UserId],[RecipeId],[Rating] FROM [Licenta].[dbo].[Favorites]"

import pandas as pd
from surprise import Dataset
from surprise import Reader

db_data = pd.read_sql(query,conn)
reader = Reader(rating_scale=(1,5))

ratings = Dataset.load_from_df(db_data[["UserId", "RecipeId", "Rating"]], reader)

from surprise import KNNBasic
from surprise import model_selection

algorithm = KNNBasic() 
metrics = model_selection.cross_validate(algorithm,ratings, measures=['MAE'],cv=10,return_train_measures=True)

import matplotlib.pyplot as plot

mae_test_knn = list(metrics['test_mae'])

mae_knn_lib =  sum(mae_test_knn) / len(mae_test_knn) 


from sklearn.model_selection import train_test_split

X = db_data[["UserId", "RecipeId"]]
y = db_data["Rating"]


from sklearn.neighbors import KNeighborsClassifier
from sklearn import preprocessing
from sklearn.metrics import mean_absolute_error

sklearn_knn_mae = []
for i in range(10):
    X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2)

    le = preprocessing.LabelEncoder()
    for user in X_train.columns:
        if X_train[user].dtype == object:
            X_train[user] = le.fit_transform(X_train[user])

    for user in X_test.columns:
        if X_test[user].dtype == object:
            X_test[user] = le.fit_transform(X_test[user])

    knn = KNeighborsClassifier(n_neighbors=10)

    knn.fit(X_train, y_train)

    y_pred = knn.predict(X_test)
    sklearn_knn_mae.append(mean_absolute_error(y_test, y_pred))

mae_knn_sklearn = sum(sklearn_knn_mae)/len(sklearn_knn_mae)



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
                    else:
                        common[r] = d[o][r]
    return dict(common), set(recommendations)
                

def run_iteration(d):
    mae_total = 0
    nr = len(d)
    for user in d:
        common, recommendations = get_recommendations(user,d)
        common = dict(common)
        sum = 0
        for r in d[user]:
            if r in common.keys():
                sum+= abs(common[r]-d[user][r])
        if len(common) > 0:
            mae = sum/ len(common)
            mae_total+= mae
        else:
            nr-=1

    mae_total = mae_total / nr
    return mae_total

mae_knn_propriu = []
for i in range(10):
    train, test = train_test_split(db_data, test_size=0.2)
    d = {}
    for i in test['UserId'].unique():
        d[i] = {test['RecipeId'][j]: test['Rating'][j] for j in test[test['UserId']==i].index}
    mae = run_iteration(d)
    mae_knn_propriu.append(mae)

iterations = ['It. '+ str(i) for i in range(10) ]

maes_names = ['Surprise', 'Sklearn', 'Propriu']

# plot.plot(iterations, mae_knn_propriu  )
# plot.xlabel("Algoritmi")
# plot.ylabel("MAE")
# plot.show()
mae_total = sum(mae_knn_propriu) / len(mae_knn_propriu)

maes = []
maes.append(mae_knn_lib)
maes.append(mae_knn_sklearn)
maes.append(mae_total)

plot.plot( iterations, sklearn_knn_mae, marker ='.' , color='blue', label='Sklearn', linewidth=2)
plot.plot( iterations, mae_test_knn, marker='.', color='olive', linewidth=2, label="Surprise")
plot.plot( iterations, mae_knn_propriu, marker='.', color='red', linewidth=2, label="Propriu")
plot.show()


#maes_names = ['Surprise', 'Sklearn', 'Propriu']

plot.plot(maes_names, maes, 'go',  )
plot.xlabel("Algoritmi")
plot.ylabel("MAE")
plot.show()
    
    
    



