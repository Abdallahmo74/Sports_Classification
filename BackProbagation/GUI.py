import tkinter as tk
from tkinter import ttk

class MyGUI:
    def __init__(self):
        self.Window = tk.Tk()
        self.Window.geometry("800x500")
        # Hidden Layers
        self.lable1 = tk.Label(self.Window, text="Number of Hidden Layers")
        self.lable1.pack()
        self.Text1 = tk.IntVar()
        self.textbox1 = ttk.Entry(self.Window, textvariable=self.Text1)
        self.textbox1.pack()

        # Number of neurons
        self.lable2 = tk.Label(self.Window, text="Number of Neurons")
        self.lable2.pack()
        self.Text2 = tk.StringVar()
        self.textbox2 = ttk.Entry(self.Window, textvariable=self.Text2)
        self.textbox2.pack()



        # Learning rate
        self.lable3 = tk.Label(self.Window, text="Enter Learning Rate")
        self.lable3.pack()
        self.Text3 = tk.DoubleVar()
        self.textbox3 = ttk.Entry(self.Window, textvariable=self.Text3)
        self.textbox3.pack()


        # Number Of Epochs
        self.lable4 = tk.Label(self.Window, text="Enter Number Of Epochs")
        self.lable4.pack()
        self.Text4 = tk.IntVar()
        self.textbox4 = ttk.Entry(self.Window, textvariable= self.Text4)
        self.textbox4.pack()



        #Activation Function
        self.lable5 = tk.Label(self.Window, text="Activation Function")
        self.lable5.pack()
        self.Selected_Feature1 = tk.StringVar()
        self.CB1 = ttk.Combobox(self.Window, textvariable=self.Selected_Feature1)
        self.CB1['values'] = ["Sigmoid Function", "Hyperbolic Tangent"]
        self.CB1['state'] = 'readonly'
        self.CB1.pack()


        # Adding Bais or not
        self.Bais = tk.StringVar()
        self.checkbox = ttk.Checkbutton(self.Window, text="Bais", variable=self.Bais, onvalue='True', offvalue='False')
        self.checkbox.pack()


        # Button
        self.btn = ttk.Button(self.Window, text="Save Values", command= self.Window.destroy)
        self.btn.pack()


        self.Window.mainloop()
