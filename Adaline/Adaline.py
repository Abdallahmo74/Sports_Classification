import pandas as pd
import numpy as np
import GUI
import matplotlib.pyplot as plt

def Normaliz(data):
    data = (data - np.array(data).min()) / (np.array(data).max()-np.array(data).min())
    return data

#GUI
Main_GUI = GUI.MyGUI()


#Reading Folder
data = pd.read_csv('C:/Users/abdal/OneDrive/Desktop/Lab3/penguins.csv')


#Preprocessing
#Filling Null Values with Previous values
data = data.fillna(method = 'pad')

#Converting Ordinal data to Numeric values
datamapping={
    "male":1,
    "female":2
}
data['gender']= data['gender'].map(datamapping)

#Normalization
data["bill_length_mm"] = Normaliz(data["bill_length_mm"])
data["bill_depth_mm"] = Normaliz(data["bill_depth_mm"])
data["flipper_length_mm"] = Normaliz(data["flipper_length_mm"])
data["gender"] = Normaliz(data["gender"])
data["body_mass_g"] = Normaliz(data["body_mass_g"])

#For shuffeling data
AdelieData = data[0:50]
GentooData = data[50:100]
Chinstrap = data[100:150]

shuffled_AdelieData = AdelieData.sample(frac=1)
shuffled_GentooData = GentooData.sample(frac=1)
shuffled_Chinstrap = Chinstrap.sample(frac=1)

Datamap1 = {
        "Adelie" : 0,
        "Gentoo" : 0,
        "Chinstrap": 0
}

#Visualization

#(X1 , X2)
if(Main_GUI.Visualization1.get() == "true"):
    figure , axis = plt.subplots(2,5)
    axis[0,0].scatter(shuffled_AdelieData['bill_length_mm'], shuffled_AdelieData['bill_depth_mm'], color="red")
    axis[0,0].scatter(shuffled_GentooData['bill_length_mm'], shuffled_GentooData['bill_depth_mm'], color="yellow")
    axis[0,0].scatter(shuffled_Chinstrap['bill_length_mm'], shuffled_Chinstrap['bill_depth_mm'], color="green")
    axis[0, 0].set_title("X1 , X2")

    axis[0, 1].scatter(shuffled_AdelieData['bill_length_mm'], shuffled_AdelieData['flipper_length_mm'], color="red")
    axis[0, 1].scatter(shuffled_GentooData['bill_length_mm'], shuffled_GentooData['flipper_length_mm'], color="yellow")
    axis[0, 1].scatter(shuffled_Chinstrap['bill_length_mm'], shuffled_Chinstrap['flipper_length_mm'], color="green")
    axis[0, 1].set_title("X1 , X3")

    axis[0, 2].scatter(shuffled_AdelieData['bill_length_mm'], shuffled_AdelieData['gender'], color="red")
    axis[0, 2].scatter(shuffled_GentooData['bill_length_mm'], shuffled_GentooData['gender'], color="yellow")
    axis[0, 2].scatter(shuffled_Chinstrap['bill_length_mm'], shuffled_Chinstrap['gender'], color="green")
    axis[0, 2].set_title("X1 , X4")

    axis[0, 3].scatter(shuffled_AdelieData['bill_length_mm'], shuffled_AdelieData['body_mass_g'], color="red")
    axis[0, 3].scatter(shuffled_GentooData['bill_length_mm'], shuffled_GentooData['body_mass_g'], color="yellow")
    axis[0, 3].scatter(shuffled_Chinstrap['bill_length_mm'], shuffled_Chinstrap['body_mass_g'], color="green")
    axis[0, 3].set_title("X1 , X5")

    axis[0, 4].scatter(shuffled_AdelieData['bill_depth_mm'], shuffled_AdelieData['flipper_length_mm'], color="red")
    axis[0, 4].scatter(shuffled_GentooData['bill_depth_mm'], shuffled_GentooData['flipper_length_mm'], color="yellow")
    axis[0, 4].scatter(shuffled_Chinstrap['bill_depth_mm'], shuffled_Chinstrap['flipper_length_mm'], color="green")
    axis[0, 4].set_title("X2 , X3")

    axis[1, 0].scatter(shuffled_AdelieData['bill_depth_mm'], shuffled_AdelieData['gender'], color="red")
    axis[1, 0].scatter(shuffled_GentooData['bill_depth_mm'], shuffled_GentooData['gender'], color="yellow")
    axis[1, 0].scatter(shuffled_Chinstrap['bill_depth_mm'], shuffled_Chinstrap['gender'], color="green")
    axis[1, 0].set_title("X2 , X4")

    axis[1, 1].scatter(shuffled_AdelieData['bill_depth_mm'], shuffled_AdelieData['body_mass_g'], color="red")
    axis[1, 1].scatter(shuffled_GentooData['bill_depth_mm'], shuffled_GentooData['body_mass_g'], color="yellow")
    axis[1, 1].scatter(shuffled_Chinstrap['bill_depth_mm'], shuffled_Chinstrap['body_mass_g'], color="green")
    axis[1, 1].set_title("X2 , X5")

    axis[1, 2].scatter(shuffled_AdelieData['flipper_length_mm'], shuffled_AdelieData['gender'], color="red")
    axis[1, 2].scatter(shuffled_GentooData['flipper_length_mm'], shuffled_GentooData['gender'], color="yellow")
    axis[1, 2].scatter(shuffled_Chinstrap['flipper_length_mm'], shuffled_Chinstrap['gender'], color="green")
    axis[1, 2].set_title("X3 , X4")

    axis[1, 3].scatter(shuffled_AdelieData['flipper_length_mm'], shuffled_AdelieData['body_mass_g'], color="red")
    axis[1, 3].scatter(shuffled_GentooData['flipper_length_mm'], shuffled_GentooData['body_mass_g'], color="yellow")
    axis[1, 3].scatter(shuffled_Chinstrap['flipper_length_mm'], shuffled_Chinstrap['body_mass_g'], color="green")
    axis[1, 3].set_title("X3 , X5")

    axis[1, 4].scatter(shuffled_AdelieData['gender'], shuffled_AdelieData['body_mass_g'], color="red")
    axis[1, 4].scatter(shuffled_GentooData['gender'], shuffled_GentooData['body_mass_g'], color="yellow")
    axis[1, 4].scatter(shuffled_Chinstrap['gender'], shuffled_Chinstrap['body_mass_g'], color="green")
    axis[1, 4].set_title("X4 , X5")

    plt.show()




if(Main_GUI.Selected_Feature3.get() == "C1 & C2"):
    TrainingData = shuffled_AdelieData[0:30]
    TrainingData = TrainingData.append(shuffled_GentooData[0:30])
    TestingData =  shuffled_AdelieData[30:50]
    TestingData =  TestingData.append(shuffled_GentooData[30:50])
    Datamap1 = {
        "Adelie" : 1,
        "Gentoo" : -1
    }

elif(Main_GUI.Selected_Feature3.get() == "C1 & C3"):
    TrainingData = shuffled_AdelieData[0:30]
    TrainingData = TrainingData.append(shuffled_Chinstrap[0:30])
    TestingData =  shuffled_AdelieData[30:50]
    TestingData = TestingData.append(shuffled_Chinstrap[30:50])
    Datamap1 = {
        "Adelie": 1,
        "Chinstrap": -1
    }


elif(Main_GUI.Selected_Feature3.get() == "C2 & C3"):
    TrainingData = shuffled_GentooData[0:30]
    TrainingData = TrainingData.append(shuffled_Chinstrap[0:30])
    TestingData =  shuffled_GentooData[30:50]
    TestingData = TestingData.append(shuffled_Chinstrap[30:50])
    Datamap1 = {
        "Gentoo": 1,
        "Chinstrap": -1
    }

TrainingData['species'] = TrainingData['species'].map(Datamap1)
TestingData['species'] = TestingData['species'].map(Datamap1)
t = np.array(TrainingData['species'])



#Model
#Initializing Weights

b = 0
if(Main_GUI.Bais.get() == "false"):
    b =0


elif(Main_GUI.Bais.get() == "true"):
    b=1



#Feature 1 Selection

if(Main_GUI.Selected_Feature1.get() == 1):
    TrainFeature1 = np.array(TrainingData['bill_length_mm'])
    TestFeature1 = np.array(TestingData['bill_length_mm'])
    Feature1_plt = "bill_length_mm"

elif(Main_GUI.Selected_Feature1.get() == 2):
    TrainFeature1 = np.array(TrainingData['bill_depth_mm'])
    TestFeature1 = np.array(TestingData['bill_depth_mm'])
    Feature1_plt = "bill_depth_mm"

elif(Main_GUI.Selected_Feature1.get() == 3):
    TrainFeature1 = np.array(TrainingData['flipper_length_mm'])
    TestFeature1 = np.array(TestingData['flipper_length_mm'])
    Feature1_plt = "flipper_length_mm"

elif(Main_GUI.Selected_Feature1.get() == 4):
    TrainFeature1 = np.array(TrainingData['gender'])
    TestFeature1 = np.array(TestingData['gender'])
    Feature1_plt = "gender"

elif(Main_GUI.Selected_Feature1.get() == 5):
    TrainFeature1 = np.array(TrainingData['body_mass_g'])
    TestFeature1 = np.array(TestingData['body_mass_g'])
    Feature1_plt = "body_mass_g"



#Feature 2 Selection

if(Main_GUI.Selected_Feature2.get() == 1):
    TrainFeature2 = np.array(TrainingData['bill_length_mm'])
    TestFeature2 = np.array(TestingData['bill_length_mm'])
    Feature2_plt = "bill_length_mm"

elif(Main_GUI.Selected_Feature2.get() == 2):
    TrainFeature2 = np.array(TrainingData['bill_depth_mm'])
    TestFeature2 = np.array(TestingData['bill_depth_mm'])
    Feature2_plt = "bill_depth_mm"

elif(Main_GUI.Selected_Feature2.get() == 3):
    TrainFeature2 = np.array(TrainingData['flipper_length_mm'])
    TestFeature2 = np.array(TestingData['flipper_length_mm'])
    Feature2_plt = "flipper_length_mm"

elif(Main_GUI.Selected_Feature2.get() == 4):
    TrainFeature2 = np.array(TrainingData['gender'])
    TestFeature2 = np.array(TestingData['gender'])
    Feature2_plt = "gender"


elif(Main_GUI.Selected_Feature2.get() == 5):
    TrainFeature2 = np.array(TrainingData['body_mass_g'])
    TestFeature2 = np.array(TestingData['body_mass_g'])
    Feature2_plt = "body_mass_g"


#Training

W1 = 0.1
W2 = 0.1
y = np.zeros(len(TrainFeature1))
inputX1 = np.array([TrainFeature1,TrainFeature2])
range1 = Main_GUI.Text2.get()
range2 = len(TrainFeature1)
Ln = Main_GUI.Text1.get()
X0 = 1
Error_Arr = np.zeros(60)
MSE =0
for j in range(0,range1):
    for i in range(0, range2):
        y[i] =  (W1*TrainFeature1[i]) + (W2*TrainFeature2[i]) + b
        if(y[i] != t[i]):
            Error = t[i] - y[i]
            Error_Arr[i] = Error
            W1 = W1 + ((Ln * Error) * TrainFeature1[i])
            W2 = W2 + ((Ln * Error) * TrainFeature2[i])
            if(Main_GUI.Bais.get() == "true"):
                b = b + ((Ln * Error) * X0)

        else:
            continue


    for k in range(0 , range2):
        y[i] = (W1 * TrainFeature1[i]) + (W2 * TrainFeature2[i]) + b
        if (y[i] != t[i]):
            Error = t[i] - y[i]
            Error_Arr[i] = Error
        MSE += 0.5 * (Error_Arr[k] * Error_Arr[k])


    MSE = (1 / range2) * MSE


    if(MSE < Main_GUI.Text3.get()):
        break
    else:
        MSE =0





#Testing

tstrange = len(TestFeature1)
y2 = np.zeros(len(TestFeature1))
CorrectTest = 0
t1 = np.array(TestingData['species'])

for z in range(0 , tstrange):

    y2[z] = (W1*TestFeature1[z]) + (W2*TestFeature2[z]) + b

    if (y2[z] >= 0):
        y2[z] = 1
    else:
        y2[z] = -1

    if(y2[z]==t1[z]):
        CorrectTest = CorrectTest+1



TestingAccuracy = float(CorrectTest) / float(len(TestFeature1))

print(TestingAccuracy*100)


plt.scatter(TestFeature1[0:20] , TestFeature2[0:20] , color = "green")
plt.scatter(TestFeature1[20:40] , TestFeature2[20:40] , color="red")
plt.xlabel(Feature1_plt)
plt.ylabel(Feature2_plt)

X1min = TestFeature1.min()
X2min = TestFeature2.min()
if(X1min < X2min):
    X1_Graph = X1min
else:
    X1_Graph = X2min

X1max = TestFeature1.max()
X2max = TestFeature2.max()
if(X1max > X2max ):
    X2_Graph = X1max
else:
    X2_Graph = X2max

Y1_Graph = -((W1*X1_Graph)/W2) - (b/W2)
Y2_Graph = -((W1*X2_Graph)/W2) - (b/W2)
X_G = np.array([X1_Graph , X2_Graph])
Y_G = np.array([Y1_Graph,Y2_Graph])

plt.plot(X_G , Y_G)
plt.show()



















