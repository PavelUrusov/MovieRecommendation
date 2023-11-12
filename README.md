# Movie Recommendation System

This repository contains code for training and deploying a movie recommendation system. The system consists of two main components: a model training module and an API for making movie recommendations.

## Prerequisites

Before running the code, ensure that you have the following prerequisites:

- [.NET Core SDK](https://dotnet.microsoft.com/download) installed
- [Microsoft ML.NET](https://dotnet.microsoft.com/apps/machinelearning-ai/ml-dotnet) library installed
- [ASP.NET Core](https://dotnet.microsoft.com/apps/aspnet) installed for running the API

## Installation

1. Clone the repository:

   ```shell
   git clone https://github.com/PavelUrusov/MovieRecommendation.git
2. Build the solution:
    ```shell
    cd MovieRecommendation
    dotnet build

# Usage

## Data Preparation
The movie recommendation system requires a training dataset in the form of a text file. Follow these steps to prepare your data:
1.  Create a text file containing the movie ratings data.Each line should represent a single movie rating and follow the format: `<UserId> <MovieId> <Rating>`.
2.  Update the `DataTrainingConfig` class in the `MovieRecommendation.TrainingModel.Common.Configs` namespace with the path and configuration of your data.

## Model Training
To train the movie recommendation model using grid search optimization, follow these steps:
1. Update the `GridSearchConfig` class in the `MovieRecommendation.TrainingModel.Common.Configs` namespace with the desired configuration for the grid search.
2. Run the following command to start the grid search optimization:
    ```shell
    cd MovieRecommendation.TrainingModel
    dotnet run
    ```
   This will train and evaluate multiple models with different hyperparameters, and save the best model.
3. The best model found during the grid search optimization is saved as a ZIP file at `MovieRecommendation.TrainingModel/best-model.zip`. The evaluation metrics, including
RSquared and RootMeanSquaredError, are logged during the training process.

## API
The API provides endpoints for making movie recommendations based on the trained model. 
Start the API by running the following command:
    ```shell
    cd MovieRecommendation.API
    dotnet run
    ```
The API will be available at https://localhost:8000.

## Making Recommendations
To make movie recommendations using the API, send a POST request to the /api/recommendations endpoint with the following payload:
```json
{
    "userId": 1,
    "numRecommendations": 5
}
```
Replace userId with the desired user ID and numRecommendations with the number of movie recommendations to retrieve.