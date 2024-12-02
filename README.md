# BloodBankApp
Prepare Data: 
	AWS DynamoDB (Link to the sample data here)
API Implementation: 
	C# .NET 8.0 (Class Library), 
	C# ASP.NET 8.0 Web API Service (Web API)
	Using the repository pattern
	Implemented DTO classes
		At least 3 DTO classes for each controller
		Use AutoMapper Mapping profile
	For each controller (BloodType and DonationCenter):
		Http Get method, GetAll & GetByID
		Http Post method
		Http Put method
		Http Patch method
		Http Delete method
Publish API: 
	AWS ECS with Fargate for containerization and publication, 
	AWS ECR to push the image
Manage API:
	Manage your published API using Google APIgee
	Your API proxy policy should include “Verify API key”
	Generate portal to support your potential API client(s) 
Consume API:
	Implemented a ASP.NET MVC Core Web App (Model-View-Controller) 8.0 client to consume the published API
		Instantiated a HttpClient object 
		Invoked all 6 methods provided in the API


## How to run the project:
---
First populate the sample data in your DynamoDB table 'Blood_Donation_Centers'.

Open the project, and ensure Docker Desktop is running and is using 'Linux Containers'
![Screenshot 2024-12-02 154727](https://github.com/user-attachments/assets/2bcdfbc8-abf7-4cf6-8d76-2d97e3e7ad29)
Grab your AWS IAM user credentials and put them into your 'appsettings.json' under the 'BloodBankAPI' project:
![Screenshot 2024-12-02 160806](https://github.com/user-attachments/assets/2fd735bf-4284-402c-bfb3-8ba10679a9f1)
After the project loads, right click BloodBankAPI and choose 'Publish to AWS'
![Screenshot 2024-12-02 155024](https://github.com/user-attachments/assets/09d49b81-0b0c-491b-b880-c58af0e82359)
You'll arrive at this screen:
![Screenshot 2024-12-02 155121](https://github.com/user-attachments/assets/cb866959-04ba-4e86-a55f-9f0c18c16d68)
Choose the option selected in the screenshot: 'ASP.NET Core App to Amazon ECS using AWS Fargate'.
But before you click 'Publish', edit the settings to the following:
![Screenshot 2024-12-02 155405](https://github.com/user-attachments/assets/5dfcf319-ff30-40de-9cae-042b571f444f)
	Ensure you use the 'ecsTaskExecutionRole' as shown in the screenshot, as well as creating a 'New VPC' (to get this to work you might want to first select an existing VPC, choose one, go back and click 'New VPC', this circumvents the Security Group errors)
Click Publish and wait until the progress shows 36/38:
![Screenshot 2024-11-30 160041](https://github.com/user-attachments/assets/784906e5-2b4a-4586-8531-4e00b4691cdd)
Once it reaches 36/38, go to your 
AWS Explorer Toolbar -> expand 'Amazon Elastic Container Service' -> expand 'Clusters' -> Double click the cluster you just created by publishing:
![Screenshot 2024-12-02 161033](https://github.com/user-attachments/assets/0313a103-1f9e-410f-a2b1-ed4b9e91714d)
![Screenshot 2024-11-27 181350](https://github.com/user-attachments/assets/c8de0b73-4847-4a74-976b-60f0e73dc519)
	Another Screenshot:
![Screenshot 2024-12-01 212334](https://github.com/user-attachments/assets/43bdada7-0031-4f64-9cd4-80d4f97e022e)
If you see the two links there created by the Load Balancer, your API is running!
Next let's test if it's running correctly:
You can either go to the link provided by the load balancer and add '/api/DonationCenter' or '/api/BloodType/ON_001' in your browser OR you can use 'Postman':
![Screenshot 2024-11-27 181217](https://github.com/user-attachments/assets/3f666022-1b37-434c-9784-e55ea40842bd)
	Here we can see the API is working correctly!
To run the MVC portion of the API, first head over to 'BloodBankMVC/Services/BloodTypeService.cs' at the top change the 'BaseUrl' value to the link to the published API and add 'api/BloodType'
![Screenshot 2024-12-02 161453](https://github.com/user-attachments/assets/94578a09-6a7a-433c-a3ef-844d9e359f65)
Same thing for DonationCenter, but this time add 'api/DonationCenter'.
![Screenshot 2024-12-02 161551](https://github.com/user-attachments/assets/fc48939d-1521-4ca9-998f-35f4afa8066b)

Reminder, you can also load up the API locally (using http), without doing the ECS stuff by opening a new Visual Studio instance, one for the API, one for the MVC.
![Screenshot 2024-12-02 161725](https://github.com/user-attachments/assets/892f7f75-0c80-4bbc-b6ce-65ea1ef817b0)
	Remember to change the BaseUrl to this if running locally (for both BloodTypeService and DonationCenter adding the api/DonationCenter and api/BloodType)
Change the BloodBankMVC to 'https', and run (after opening another Visual Studio)
![Screenshot 2024-12-02 162212](https://github.com/user-attachments/assets/6c26ca95-4eb8-41f9-b9e3-0c778ceba765)
Home Page:
![Screenshot 2024-12-02 162300](https://github.com/user-attachments/assets/5993fa56-aa65-4db5-b49c-f567f436826c)
Donation Center Index Page:
![Screenshot 2024-12-02 162309](https://github.com/user-attachments/assets/09ec6d3f-db0d-465c-a3c0-4023084fa2f8)
Add New Donation Center:
![Screenshot 2024-12-02 162320](https://github.com/user-attachments/assets/1acb91cf-0916-4d24-a95d-b03a326c71ff)
Details for Donation Center:
![Screenshot 2024-12-02 162332](https://github.com/user-attachments/assets/e6f2683b-2306-4b4a-a185-fd55dc475be3)
Edit a Donation Center:
![Screenshot 2024-12-02 162644](https://github.com/user-attachments/assets/07b24b22-ec98-4c03-a246-7a88b129533c)
Blood Types Index Page:
![Screenshot 2024-12-02 162348](https://github.com/user-attachments/assets/bf849803-6c30-49ea-8dd0-c6fe0e301032)
Create a New Blood Type:
![Screenshot 2024-12-02 162358](https://github.com/user-attachments/assets/fae79054-20c0-43b9-bf50-9194d1f51fc7)
Blood Type Details Page:
![Screenshot 2024-12-02 162406](https://github.com/user-attachments/assets/10a8110b-6603-4d1b-a352-ceeb9e666594)
Edit Blood Type Page:
![Screenshot 2024-12-02 162414](https://github.com/user-attachments/assets/a8905df1-64f7-49a8-ae8d-bbfa64451ef8)
Update Stock Levels for a Blood Type:
![Screenshot 2024-12-02 162423](https://github.com/user-attachments/assets/3fd758b6-57ef-4cde-be47-f533b65d8956)
Deleted Blood Type:
![Screenshot 2024-12-02 162449](https://github.com/user-attachments/assets/dc6b8c82-2090-481f-9d2a-5c33d66ae206)
Adding New Blood Types:
![Screenshot 2024-12-02 162544](https://github.com/user-attachments/assets/aebc1526-3487-4634-a71a-d95fa77f1193)
![Screenshot 2024-12-02 162625](https://github.com/user-attachments/assets/7fe2e131-8fd2-4d10-b7a9-44298c3b0a30)
