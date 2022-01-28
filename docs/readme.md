# CQRS Sample Project
This project sample cqrs project


## Project Structure

|   Project Solution Name   |   Purpose |   https   |   http    |
|---------------------------|-----------|-----------|-----------|
| CQRS.Database.Creator     |  Creates the database to be used for the demo project. (only once)                            |   [https://localhost:7174](https://localhost:7174)    |   [http://localhost:5174](http://localhost:5174)    |
| CQRS.WriteCategory.Api    |  Used to add category in demo project.                                                        |   [https://localhost:7080](https://localhost:7080)    |   [http://localhost:5080](http://localhost:5080)    |
| CQRS.WriteProduct.Api     |  Used to add product in demo project.                                                         |   [https://localhost:7014](https://localhost:7014)    |   [http://localhost:5014](http://localhost:5014)    |
| CQRS.Distributor.App      |  Distributes messages waiting to be scattered in demo project.                                |   [https://localhost:7014](https://localhost:7014)    |   [http://localhost:5014](http://localhost:5014)    |
| CQRS.CategoryConsumer.App |  Reads categories waiting to be written to elastic search and completes the write operation.  |   [https://localhost:7154](https://localhost:7154)    |   [http://localhost:5154](http://localhost:5154)    |
| CQRS.ProductConsumer.App  |  Reads products waiting to be written to elastic search and completes the write operation.    |   [https://localhost:7217](https://localhost:7217)    |   [http://localhost:5217](http://localhost:5217)    |
| CQRS.ReadCategory.Api     |  Used to read category in demo project.                                                       |   [https://localhost:7145](https://localhost:7145)    |   [http://localhost:5145](http://localhost:5145)    |
| CQRS.ReadProduct.Api      |  Used to read product in demo project.                                                        |   [https://localhost:7179](https://localhost:7179)    |   [http://localhost:5179](http://localhost:5179)    |


## Usage Same Technologies

