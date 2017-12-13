# voa-ps-data-stub


This is a development tool to stub the data calls to http://voaintegration.cloudapp.net:60087/
Simply having this running as a service on your local development machine will simulate real calls without the dependency of the 3rd party.



## Requirements 

**Visual Studio 2017 -** Required to make project related changes

**.NET Core -** .NET Core can be included in the VS install as a workload however only the CLI is required to run the solution. 
https://www.microsoft.com/net/core?WT.mc_id=Blog_CENews_Announce_CEA#windowscmd


## Running the solution

Open a CMD and navigate to the  **voa-ps-data-stub/voa-ps-data-stub** directory and simply run
`dotnet restore` and `dotnet run`

You can run it via Visual Studio (not recommended) as per normal.


## Contributing 

#### How it works:

The structure of how the requests are translated to data is very simple. 


>http://localhost:60087/ **cca-case-management-api/cca_case/check** / *CHK1000456*
>**BOLD** translates to the directory structure, *Italics* translates to the file eg: CHK1000456.json


#### Changing data:

To modify the return JSON for this example, the file CHK1000456.json is located in
**voa-ps-data-stub/voa-ps-data-stub/Resources/cca-case-management-api/cca_case/check/CHK1000456.json**

#### Adding an endpoint stub:
In Visual Studio add the relevant directory structure inside the Resources directory (As per the   "How it works section above") and any relevant files.  Each .json file inside acts as a set of data, so you can create multiple .json files and the relevant one will be returned based on its name in the directory.
> **Eg:** to add a stub for the endpoint http://voaintegration.cloudapp.net:60087/some/new/endpoint/1
> Create a new directory structure like this 
> ![alt text](http://i.imgur.com/E02Gl2r.png)
> 
> That is all that is required, if it is a endpoint that should be persisted. Push the changes back to this repo.

