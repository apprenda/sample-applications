# Sample Messaging, Caching and Logging
Sample application that leverages the caching, pub/sub and logging API for .NET applications. 

#Building from Source
1. Clone the repository
2. Open the Solution with Visual Studio
3. Build the Solution
4. Build an Apprenda Archive. The structure should be as follows:
** The root UI should be the Apprenda.Messaging.Web project 
** The WCF Service layer should be the Apprenda.Messaging.Receiver project
** It is important that you deploy both components as one application because access to the Cache is scoped at an application level in this example. 
5. Deploy application to your Apprenda environment. 

#Additional Notes:
* The application was built and tested using VS 2013 and the 6.5.1 version of the Apprenda SDK and Cloud Platform.
* A built and packaged version of the application is available for download as part of this repository.
