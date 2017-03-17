# Kubernetes stand-up for Azure Container Service

Mainly a re-hash from the [Azure Docs](https://docs.microsoft.com/en-us/azure/container-service/container-service-kubernetes-walkthrough)
and some personal notes

1. Resource group creation
    ```
    SSH_PUB_KEY=</path/to/key.pub>
    SSH_PRIV_KEY=</path/to/key>
    RESOURCE_GROUP=<name for your resource group>
    LOCATION=<Azure data region>
    az group create --name=$RESOURCE_GROUP --location=$LOCATION
    ```

    I did:
    ```
    SSH_PUB_KEY=<wouldn't you like to know :d>
    SSH_PRIV_KEY=<wouldn't you like to know :d>
    RESOURCE_GROUP=k8s-resource-group
    LOCATION=westus
    ```
    *Note: I put westus as the resource group location, but a 
    resource group can contain resources located in multiple locations. 
    The resource group location is where the resource group's **metadata** is stored.*

1. Cluster creation
    ```
    DNS_PREFIX=<some-unique-value>
    CLUSTER_NAME=<cluster name>
    NUM_AGENTS=<default is 3>
    az acs create --orchestrator-type=kubernetes --resource-group $RESOURCE_GROUP --name=$CLUSTER_NAME --dns-prefix=$DNS_PREFIX --agent-count=$NUM_AGENTS --ssh-key-value=$SSH_PUB_KEY --master-count 1
    ```

    I did:
    ```
    DNS_PREFIX=will-the-federation
    CLUSTER_NAME=the-federation
    NUM_AGENTS=5
    ```

1. Install `kubectl`
    - method from Kubernetes [docs](http://kubernetes.io/docs/user-guide/prereqs/)
    - via Azure CLI 2.0: `az acs kubernetes install-cli`

1. Configure `kubectl`:
    ```
    az acs kubernetes get-credentials --resource-group=$RESOURCE_GROUP --name=$CLUSTER_NAME --ssh-key-file=$SSH_PRIV_KEY
    ```
    *Note: The azure-cli (az) is still in preview so you may experience an issue on macOS with password
    protected ssh private keys. To get around this I manually copied the .kube/config from the kube master node 
    with scp that was--removed for some reason--specified in the [**Details** section](https://docs.microsoft.com/en-us/azure/container-service/container-service-kubernetes-walkthrough#details) 
    of the docs. `scp azureuser@<YOUR_MASTER_FQDN>:.kube/config ~/.kube/config`*

# TODOS
1. Create federated cluster for multi-region deployments