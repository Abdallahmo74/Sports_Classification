import tkinter as tk
from tkinter import ttk

class MyGUI:
    def __init__(self):
        self.Window = tk.Tk()
        self.Window.geometry("800x500")
        # Feature 1
        self.lable1 = tk.Label(self.Window, text="feature 1")
        self.lable1.pack()
        self.Selected_Feature1 = tk.IntVar()
        self.CB1 = ttk.Combobox(self.Window, textvariable= self.Selected_Feature1)
        self.CB1['values'] = [1, 2, 3, 4, 5]
        self.CB1['state'] = 'readonly'
        self.CB1.pack()

        # Feature 2
        self.lable2 = tk.Label(self.Window, text="feature 2")
        self.lable2.pack()
        self.Selected_Feature2 = tk.IntVar()
        self.CB2 = ttk.Combobox(self.Window, textvariable= self.Selected_Feature2)
        self.CB2['values'] = [1, 2, 3, 4, 5]
        self.CB2['state'] = 'readonly'
        self.CB2.pack()

        # Class selection
        self.lable3 = tk.Label(self.Window, text="Select 2 Classes")
        self.lable3.pack()
        self.Selected_Feature3 = tk.StringVar()
        self.CB3 = ttk.Combobox(self.Window, textvariable= self.Selected_Feature3)
        self.CB3['values'] = ["C1 & C2", "C1 & C3", "C2 & C3"]
        self.CB3['state'] = 'readonly'
        self.CB3.pack()

        # Learning rate
        self.lable4 = tk.Label(self.Window, text="Enter Learning Rate")
        self.lable4.pack()
        self.Text1 = tk.DoubleVar()
        self.textbox1 = ttk.Entry(self.Window, textvariable=self.Text1)
        self.textbox1.pack()

        # Number Of Epochs
        self.lable5 = tk.Label(self.Window, text="Enter Number Of Epochs")
        self.lable5.pack()
        self.Text2 = tk.IntVar()
        self.textbox2 = ttk.Entry(self.Window, textvariable= self.Text2)
        self.textbox2.pack()

        #MSE
        self.lable6 = tk.Label(self.Window, text="Enter MSE Threshold")
        self.lable6.pack()
        self.Text3 = tk.DoubleVar()
        self.textbox3 = ttk.Entry(self.Window, textvariable=self.Text3)
        self.textbox3.pack()

        # Adding Bais or not
        self.Bais = tk.StringVar()
        self.checkbox = ttk.Checkbutton(self.Window,text = "Bais" , variable= self.Bais, onvalue='true', offvalue='false')
        self.checkbox.pack()


        # Visualization
        self.Visualization1 = tk.StringVar()
        self.checkbox1 = ttk.Checkbutton(self.Window, text="All Classes Visualization", variable=self.Visualization1, onvalue='true', offvalue='false')
        self.checkbox1.pack()


        # Button
        self.btn = ttk.Button(self.Window, text="Save Values", command= self.Window.destroy)
        self.btn.pack()


        self.Window.mainloop()





