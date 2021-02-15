import pyodbc

conn = pyodbc.connect('Driver={SQL Server};'
                      'Server=DESKTOP-3USCAD1;'
                      'Database=Licenta;'
                      'Trusted_Connection=yes;')

cursor = conn.cursor()

users=[]

for i in range(500):
    users.append('user'+str(i))
    command ='Insert into Users (Username) values (\'' +users[i]+'\')'
    cursor.execute(command)

conn.commit()
import random

for i in range(100000):
    username = users[random.randint(0,499)]
    recipe = random.randint(638000,639000)
    rating = random.randint(1,5)
    command ='Insert into Favorites (UserId, RecipeId, Rating) values (\'' +username+'\', ' + str(recipe) + ', ' + str(rating) + ')'
    cursor.execute(command)

conn.commit()

conn.execute('''
  ;WITH cte AS (
  SELECT UserId, RecipeId, Rating,
     row_number() OVER(PARTITION BY UserId, RecipeId, Rating ORDER BY UserId) AS [rn]
  FROM Favorites
)
DELETE cte WHERE [rn] > 1
''')

conn.commit()