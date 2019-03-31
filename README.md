# Azure Camp 2019

This is the code for azure camp 2019 in Calgary, Alberta. The theme of this camp is around containers and Kubernetes. For good measure we're adding a little bit of AI into the mix because that's cool these days. 

Our application is an application form for a job posting site. We're going to allow people to post jobs on the site and then we'll display them for other to apply. This system is built on top of a cluster of microservices, again because that's cool. 

If you're actually considering building a system on microservices it is best to do a bunch of research up front and have a clear understanding of what the advantages and disadvantages of microservices are. https://dwmkerr.com/the-death-of-microservice-madness-in-2018/ is a pretty good article to get you started. 

## Services

1. Salary prediction service - this AI based service will rate your offered salary against the industry average for your province. 

2. UI This is the visual front end for your application. Really simple, just a couple of forms. 

3. Model builder - builds the AI model for the salary prediction service. 

4. Social poster - posts messages about new job posting on social media

## Infrastructure

We are designing the system to run atop of a series of containers hosted in Kubernetes which we'll be calling K8s to save typing. 

## Prerequisites

Most of the tools we're using can be installed quickly on the day of the camp but it never hurts to be prepared. These are some of the things you can install ahead of time. 

1. The Azure CLI ([Any platform](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest)) - for accessing azure
2. Docker ([Windows](https://runnable.com/docker/install-docker-on-windows-10) | [OSX](https://runnable.com/docker/install-docker-on-macos) | [Linux](https://runnable.com/docker/install-docker-on-linux)) - for running containers locally
3. Kubectrl ([Any platform](https://kubernetes.io/docs/tasks/tools/install-kubectl/#install-kubectl)) - for interacting with kubernetes

## Why containers?

That's a great question. Maybe we should take one step back from that and talk about what containers are. 

Years ago computers didn't have operating systems - they were dumb boxes into which you'd feed some input in the form of a program and then you'd wait for it to finish. They were also crazy mad expensive. In order to not waste money by having these monsters sit idle people started to build programs which would remain resident on the computers and which would feed new programs into the computers. 

These were the first time sharing systems. As time progressed and computers got faster sometimes it was desirable to run multiple applications at once. Of course running multiple applications at once could introduce all sorts of instabilities. What if two apps needed the same resource at the same time? How could you best share the compute resources? What if two applications used the same shared libraries but different versions?

Bit of a nightmare really. Doubly so when you consider shipping you application into an unknown environment with goodness knows what libraries installed. 

Containers introduce a layer of isolation which is greater than that of a simple process. This might sound a lot like a virtual machine but containers actually sit in a sweet spot where they can be way more efficient than a full VM and way more isolated than a process. They give you the confidence that you application will perform the same way in production as it does in test and even on your local machine. 

Let's start with building an application. Then we'll containerize it for deployment. 

## Building the Application




## Original plan
1. Create an app which listens to an Azure storage queue
2. Add docker support and run on your local machine
3. Check it into GitHub
4. Hook up an azure DevOps account to it and do a build
5. Check the security of your application during builds and deployment: DevOps security or DevSecOps.
6. Hook DevOps up to your Azure account and push the container to the docker registry in Azure
7. Using an ARM template build a reproducible Azure environment
8. Deploy the container to AKS and use DevOps release pipelines to deploy a new version
