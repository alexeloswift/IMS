Introduction

Throughout your career in software design and development, you will be asked to create applications with various features and functionality based on business requirements. When a new system is developed, the process typically begins with a business analyst gathering and writing the business requirements with the assistance of subject matter experts from the business. A system analyst then works with several application team members and others to formulate a solution based on the requirements. As a developer, you would then create a design document from the solution and finally develop the system based on your design document.

For this assessment, you will create a C# application using the solution statements provided in the requirements section.

The skills you showcase in your completed application will be useful in responding to technical interview questions for future employment. This application may also be added to your portfolio to show to future employers.

Note: The preferred integrated development environment (IDE) for this assignment is Visual Studio. Refer to the course for instructions on how to install and use this application. If you choose to use another IDE, you must export your project into Visual Studio format for submission.

Your submission should include a .zip file with all the necessary code files to compile, support, and run your application. The .zip file submission must also keep the project file and folder structure intact for the Visual Studio IDE.
Scenario
You are working for a small manufacturing organization that has outgrown its current inventory system. They have been using a spreadsheet program to manually enter inventory additions, deletions, and other data from a paper-based system but would now like you to develop a more sophisticated inventory program.

They have provided you with a mock-up of the user interface to use in the design and development of the system (see the attached "GUI Mock-Up") and a class diagram to assist you in your work (see the attached "UML Class Diagram"). The organization also has specific business requirements that must be included as part of the application. A system analyst from your company created the solution statements outlined in the requirements section based on the manufacturing organization's business requirements. You will use these solution statements to develop your application.
Requirements
Your submission must be your original work. No more than a combined total of 30% of a submission and no more than a 10% match to any one individual source can be directly quoted or closely paraphrased from sources, even if cited correctly. The similarity report that is provided when you submit your task that can be used as a guide.



You must use the rubric to direct the creation of your submission because it provides detailed criteria that will be used to evaluate your work. Each requirement below may be evaluated by more than one rubric aspect. The rubric aspect titles may contain hyperlinks to relevant portions of the course.



Tasks may not be submitted as cloud links, such as links to Google Docs, Google Slides, OneDrive, etc., unless specified in the task requirements. All other submissions must be file types that are uploaded and submitted as attachments (e.g., .docx, .pdf, .ppt).



Create a C# application based on the attached "GUI Mock-Up" and the attached "UML Class Diagram" by doing the following:



I. User Interface

A.  Create a main form showing the following controls:

•   buttons for "Add," "Modify," "Delete," "Search" for parts and products, and "Exit"

•   lists for parts and products

•   text boxes for searching for parts and products

•   title labels for parts, products, and the application title


B.  Create an "Add Part" form showing the following controls:

•   radio buttons for "In-House" and "Outsourced" parts

•   buttons for "Save" and "Cancel"

•   text boxes for ID, name, inventory level, price, max and min values, and company name or machine ID

•   labels for ID, name, inventory level, price or cost, max and min values, the form title, and company name or machine ID


C.  Create a "Modify Part" form with fields that populate with data from an existing part showing the following controls:

•   radio buttons for "In-House" and "Outsourced" parts

•   buttons for "Save" and "Cancel"

•   text boxes for ID, name, inventory level, price, max and min values, and company name or machine ID

•   labels for ID, name, inventory level, price, max and min values, the form title, and company name or machine ID


D.  Create an "Add-Product" form showing the following controls:

•   buttons for "Save," "Cancel," "Add" part, and "Delete" part

•   text boxes for ID, name, inventory level, price, and max and min values

•   labels for ID, name, inventory level, price, max and min values, and the form title

•   a grid view for all parts

•   a grid view for parts associated with the product

•   a "Search" button and a text field with an associated list for displaying the results of the search


E.  Create a "Modify Product" form with fields that populate with data from an existing product showing the following controls:

•   buttons for "Save," "Cancel," "Add" part, and "Delete" part

•   text boxes for ID, name, inventory level, price, and max and min values

•   labels for ID, name, inventory level, price, max and min values, and the "all candidate parts" grid

•   a grid view for parts associated with the product

•   a "Search" button and a text box with an associated list for displaying the results of the search


II. Classes

F.  Create and implement the appropriate classes, methods, and properties as specified in the attached "UML Class Diagram."


G.  Add the following behaviors to the main form using the classes, methods, and properties implemented in part F:

•   redirect the user to the "Add Part," "Modify Part," "Add Product," or "Modify Product" forms

•   delete a selected part or product from the grid view

•   search for a part or product and display matching results

•   exit the main form


H.  Add the following behaviors to the part forms using the methods provided in the attached "UML Class Diagram":

1.  Add the following behaviors to the "Add Part" form:

•   select "In-House" or "Outsourced"

•   enter name, inventory level, price, max and min values, and company name or machine ID

•   save the data and then redirect to the main form

•   cancel or exit out of this form and go back to the main form

2.  Add the following behaviors to the "Modify Part" form:

•   select "In-House" or "Outsourced"

•   modify or change data values

•   save modifications to the data and then redirect to the main form

•   cancel or exit out of this form and go back to the main form


I.  Add the following behaviors to the product forms using the methods provided in the attached "UML Class Diagram":

1.  Add the following behaviors to the "Add Product" form:

•   enter name, inventory level, price, and max and min values

•   save the data and then redirect to the main form

•   associate one or more parts with a product

•   remove or disassociate a part from a product

•   cancel or exit out of this form and go back to the main form

2.  Add the following behaviors to the "Modify Product" form:

•   modify or change data values

•   save modifications to the data and then redirect to the main form

•   associate one or more parts with a product

•   remove or disassociate a part from a product

•   cancel or exit out of this form and go back to the main form


J.  Validate the user input to prevent the following four conditions:

•   non-numeric values in textboxes that expect numeric values

•   min from being greater than max; inventory from being outside the min and max bounds

•   the user deleting a part that is associated with a product

•   deleting a part or product without confirming it first


K.  Demonstrate professional communication in the content and presentation of your submission.
