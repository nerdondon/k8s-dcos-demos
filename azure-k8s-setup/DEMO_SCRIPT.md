# Demo Script

1. Run `kubectl proxy` to get local access to the k8s dashboard
1. Run deployments and provision services for test apps
    - Talking points:
        - Show YAML manifests
        - Discuss service discovery:
            - DNS labels
            - Virtual networking
            - Configuration management built on etcd cluster
    - .NET deployment
    ```
    kubectl create -f dotnet-test-app/k8s/deployment-v1.yaml --record
    ```

    - Java deployment
    ```
    kubectl create -f java-test-app/src/main/k8s/deployment.yaml --record
    ```

    - Create load-balanced services (may take a couple minutes, so set one up before demo and leave one for the demo)
    ```
    kubectl create -f dotnet-test-app/k8s/service.lb.yaml --record
    kubectl create -f java-test-app/src/main/k8s/service.lb.yaml --record
    ```
    - Demo cluster DNS automation by hitting service internally from busyboxplus pod
    ```
    kubectl run curl --image=radial/busyboxplus:curl -i --tty
    ```
    - Get service IPs with `kubectl get services`

1. Show scaling and autoscaling
1. Show rolling updates
    - Talking points:
        - Can pause rolling update for a canary test
1. Show rollback
    - Talking points:
        - Container immutability
1. Show blue-green deployment
1. Kill node to show self-healing
1. Run deployment and provision ingress for Traefik API Gateway
    - Talking points:
        - We don't want to have separate public IP's and domain names for 
        each service. Most often we want to have the services as a path 
        off a root domain
        - We can do this with an API gateway