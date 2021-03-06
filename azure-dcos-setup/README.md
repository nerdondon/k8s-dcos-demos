# DC/OS stand-up for Azure Container Service

Mainly a re-hash from the [Azure](https://docs.microsoft.com/en-us/azure/container-service/container-service-deployment)
[Docs](https://docs.microsoft.com/en-us/azure/container-service/container-service-create-acs-cluster-cli)
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
    RESOURCE_GROUP=dcos-resource-group
    LOCATION=southcentralus
    ```

1. Cluster creation
    ```
    DNS_PREFIX=<some-unique-value>
    CLUSTER_NAME=<cluster name>
    NUM_AGENTS=<default is 3>
    az acs create --orchestrator-type=dcos --resource-group $RESOURCE_GROUP --name=$CLUSTER_NAME --dns-prefix=$DNS_PREFIX --agent-count=$NUM_AGENTS --ssh-key-value=$SSH_PUB_KEY
    ```

    I did:
    ```
    DNS_PREFIX=will-starfleet
    CLUSTER_NAME=starfleet
    NUM_AGENTS=5
    ```

1. Connect to the cluster with ssh

    *Note 1: The master FQDN will be printed after running the creation commands above*

    *Note 2: Instructions are from the [Azure docs](https://docs.microsoft.com/en-us/azure/container-service/container-service-connect#connect-to-a-dcos-or-swarm-cluster)*
    - Method 1 (Azure CLI)
    ```
    az acs dcos browse --name=$CLUSTER_NAME --resource-group $RESOURCE_GROUP --disable-browser --ssh-key-file=$SSH_PRIV_KEY
    ```
    - Method 2 (manual SSH tunnel)
    ```
    ssh -fNL 8000:localhost:80 -p 2200 -i $SSH_PRIV_KEY azureuser@${DNS_PREFIX}mgmt.${LOCATION}.cloudapp.azure.com
    ```

1. Install the dcos CLI with these instructions from the [dcos docs](https://dcos.io/docs/1.8/usage/cli/install)
    - Summary of login procedure repeated here:
    ```
    # In general:
    dcos config set core.dcos_url <insert master FQDN or IP>
    # Special case for azure because of SSH tunnel:
    dcos config set core.dcos_url http://localhost:8000
    ```

1. Access management UI's
    - DC/OS: `http://localhost:8000/`
    - Marathon: `http://localhost:8000/marathon`
    - Mesos: `http://localhost:8000/mesos`