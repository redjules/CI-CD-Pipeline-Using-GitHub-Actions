# What is CI/CD?

CI (continuous integration) is the process of automatically running all of your unit tests and integration tests for each of your builds when you create a pull request your test run automatically 
CD (Continuous delivery or continuous deployment)

Continuous delivery - building and packaging up your app ready for it to be deployed on the server, it does not include deployment of the app
continuous deployment – covers deployment of your app to your target environment 

# Architecture

![image](https://github.com/user-attachments/assets/6b9e6130-d561-4071-85e2-cacb7bf7b311)


We will have github actions build out a Docker image and have it pushed to AWS ECR 

The .NET core API used for this project is a simple library app similar to Goodreads

The app has 3 layers. There are unit test for both controllers as well as service layer. For integration tests, there are tests that show you can create an author and retrieve it as well as creating a book and retrieve it to run integration tests in github actions we need to have the API running as well as the database and we do it with Docker.

We create the Dockerfile:

![image](https://github.com/user-attachments/assets/7d547844-f988-427c-b277-cc3557d069e4)


![image](https://github.com/user-attachments/assets/f19f5678-58a5-48e2-aa72-695a3040697b)


We expose port 5275 that will use later to call our API

We will use MySQL as database using the standard MySQL image

![image](https://github.com/user-attachments/assets/612d9c81-916a-4ae7-8ee2-3603c4ef932b)

We run docker-compose up

![image](https://github.com/user-attachments/assets/0fa4b9cf-610f-4f30-9a8f-d437c47701cb)

We can use Postman to be able to call the API and make sure everything works![image](https://github.com/user-attachments/assets/014c5705-1c4d-480b-a8f6-25dd01031196)

![image](https://github.com/user-attachments/assets/110c2baa-bc01-4a54-b7d8-6c30597e1dcf)

![image](https://github.com/user-attachments/assets/21931bbd-8edb-4e30-b9ba-53e1f67c42b2)

![image](https://github.com/user-attachments/assets/fda4d2a5-94c9-4cb0-a0e8-3b293d84b252)


![image](https://github.com/user-attachments/assets/7574eabb-9152-496e-934a-8ba688f79ab7)

Now we have run all our unit tests we can now run our integration tests. For that, we need to spin up a Docker container and we can do it using our Docker compose file

![image](https://github.com/user-attachments/assets/4b89a0a6-5975-4b8e-b8bf-263d9fdbc4a9)

For the CD part of the workflow, we will push the docker image that we created up to AWS ECR

![image](https://github.com/user-attachments/assets/718d56c1-2f0c-44b1-a661-9efad5ceaf84)



Create a user that has access key and secret key. We use GitHub secrets to store them (encrypted)

We run all on GitHub actions you can see all goes thru and passes. We then got a nice test report that shows our unit tests and our integration test results and in AWS  the image uploaded successfully.

![image](https://github.com/user-attachments/assets/1dbb8c1f-d3cb-4dbb-8e15-524a90161fc0)

![image](https://github.com/user-attachments/assets/c5b9fc41-e993-4b04-bc06-1c1c6caffead)
