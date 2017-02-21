# Demo Script

1. Run `kubectl proxy` to get local access to the k8s dashboard
1. Run deployments and provision services for test apps
    - .NET deployment
    ```
    kubectl create -f dotnet-test-app/k8s/deployment-v1.yaml --record
    ```

    - Java deployment
    ```
    kubectl create -f java-test-app/src/main/k8s/deployment.yaml --record
    ```

    - Talking points:
        - Show YAML manifests
        - Discuss service discovery:
            - DNS labels
            - Virtual networking and cluster IP's
            - Configuration management built on etcd cluster
1. Run deployment for Traefik API Gateway
1. Provision service that exposes gateway to internet
1. Explain scaling and show autoscaling
1. Show rolling updates
    - Talking points:
        - Can pause rolling update for a canary test
1. Show rollback
    - Talking points:
        - Container immutability
1. Show blue-green deployment
1. Kill node to show self-healing