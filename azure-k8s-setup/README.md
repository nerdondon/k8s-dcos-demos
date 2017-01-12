# Kubernetes stand-up for Azure Container Service

Mainly a re-hash from the [Azure Docs](https://docs.microsoft.com/en-us/azure/container-service/container-service-kubernetes-walkthrough)
and some personal notes

1. Resource group creation
```
RESOURCE_GROUP=<name for your resource group>
LOCATION=<Azure data region>
az group create --name=$RESOURCE_GROUP --location=$LOCATION
```

I did:
```
RESOURCE_GROUP=k8s-resource-group
LOCATION=westus
```
*Note: I put westus as the resource group location, but a 
resource group can contain resources located in multiple locations. 
The resource group location is where the resource group's **metadata** is stored.*

2. Cluster creation
```
DNS_PREFIX=<some-unique-value>
CLUSTER_NAME=<cluster name>
az acs create --orchestrator-type=kubernetes --resource-group $RESOURCE_GROUP --name=$CLUSTER_NAME --dns-prefix=$DNS_PREFIX
```

3. Install `kubectl`
    - method from Kubernetes [docs](http://kubernetes.io/docs/user-guide/prereqs/)
    - via Azure CLI 2.0: `az acs kubernetes install-cli`

4. Configure `kubectl`:
```
az acs kubernetes get-credentials --name=$CLUSTER_NAME --location=$LOCATION
```

# TODOS
1. Create federated cluster for multi-region deployments