# Azure Camp 2019

This is the code for azure camp 2019 in Calgary, Alberta. The theme of this camp is around containers and Kubernetes. For good measure we're adding a little bit of AI into the mix because that's cool these days. 

Our application is an application form for a job posting site. We're going to allow people to post jobs on the site and then we'll display them for other to apply. This system is built on top of a cluster of microservices, again because that's cool. 

If you're actually considering building a system on microservices it is best to do a bunch of research up front and have a clear understanding of what the advantages and disadvantages of microservices are. https://dwmkerr.com/the-death-of-microservice-madness-in-2018/ is a pretty good article to get you started. 

## Services

1. Salary prediction service - this AI based service will rate your offered salary against the industry average for your province. 

2. UI This is the visual front end for your application. Really simple, just a couple of forms. 

3. Model builder - builds the AI model for the salary prediction service. 
