# DC/OS stand-up for Azure Container Service

Mainly a re-hash from the [Azure](https://docs.microsoft.com/en-us/azure/container-service/container-service-deployment)
[Docs](https://docs.microsoft.com/en-us/azure/container-service/container-service-create-acs-cluster-cli)
and some personal notes

1. Resource group creation
```
SSH_KEY_LOC=<location of SSH key>
RESOURCE_GROUP=<name for your resource group>
LOCATION=<Azure data region>
az group create --name=$RESOURCE_GROUP --location=$LOCATION
```

I did:
```
SSH_KEY_LOC=<wouldn't you like to know :d>
RESOURCE_GROUP=dcos-resource-group
LOCATION=westus
```

2. Cluster creation
```
DNS_PREFIX=<some-unique-value>
CLUSTER_NAME=<cluster name>
NUM_AGENTS=<default is 3>
az acs create --orchestrator-type=dcos --resource-group $RESOURCE_GROUP --name=$CLUSTER_NAME --dns-prefix=$DNS_PREFIX --agent-count=$NUM_AGENTS  --ssh-key-value=$SSH_KEY_LOC
```

I did:
```
DNS_PREFIX=will-starfleet
CLUSTER_NAME=starfleet
NUM_AGENTS=5
```

3. Connect to the cluster with ssh
*Note 1: The master FQDN will be printed after running the creation commands above*

*Note 2: Instructions are from the [Azure docs](https://docs.microsoft.com/en-us/azure/container-service/container-service-connect#connect-to-a-dcos-or-swarm-cluster)*
```
ssh -fNL 80:localhost:80 -p 2200 -i $SSH_KEY_LOC "azureuser@${DNS_PREFIX}mgmt.${LOCATION}.cloudapp.azure.com"
```

4. Install the dcos CLI with these instructions from the (dcos docs)[https://dcos.io/docs/1.8/usage/cli/install]

4. Access management UI's
    - DC/OS: `http://localhost:80/`
    - Marathon: `http://localhost:80/marathon`
    - Mesos: `http://localhost:80/mesos`