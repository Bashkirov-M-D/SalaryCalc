# SalaryCalc
This project is made with Windows Forms GUI Class library. It consists of 5 classes (besides default Program.cs, that I did not change).

1. MainForm.cs

In this class I've implemented evrethyng connected to UI and nothing beyond that (as much as I could).

2. StaffMember.cs

This one contains all personal information about staff members.

3. SalaryCalculator

All calculations are here. Could be and should be much more effective to use this app for real purposes.

4. DBManager

Everything connected to database.

5. EventManager

Connects all other classes together.


What is good about this app:
1. Code should be easy to understand.
2. Easy to add new features.
3. Some safety nets are implemented (i.e. you can not add people without names or salary, although you can still name them like "1").

What should be improved:
1. UI.
2. More features.
3. Optimisation (number of database requests could be significantly lower).

I did not make any tests in my code because this project is relatively simple and there is not too much to test in it. 
