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
    kubectl create -f dotnet-test-app/k8s/deployment-v1.yaml --record --save-config
    ```

    - Java deployment
    ```
    kubectl create -f java-test-app/src/main/k8s/deployment.yaml --record --save-config
    ```

    - Create load-balanced services (may take a couple minutes, so set one up before demo and leave one for the demo)
    ```
    kubectl create -f dotnet-test-app/k8s/service.lb.yaml --record --save-config
    kubectl create -f java-test-app/src/main/k8s/service.lb.yaml --record --save-config
    ```
    - Demo cluster DNS automation by hitting service internally from busyboxplus pod
    ```
    kubectl run curl --image=radial/busyboxplus:curl -i --tty
    ```
    - Get service IPs with `kubectl get services`

1. Show scaling and autoscaling
    - Scale imperatively:
    ```
    kubectl scale deployment dotnet-test-app --replicas 7
    kubectl get deployment
    ```
    or declaratively by changing manifest and `kubectl apply`
    - Add an autoscaler
    ```
    kubectl create -f dotnet-test-app/k8s/hpa.yaml --record
    ```
    - Create some load
    ```
    kubectl run -i --tty load-generator --image=busybox /bin/sh
    // While in pod prompt
    while true; do wget -q -O- http://dotnet-test-app.default.svc.cluster.local; done
    ```
    - Check statuses
    ```
    kubectl get hpa
    kubectl get deployment dotnet-test-app
    ```
1. Show rolling updates
    - Simply apply changes in the manifest from version control and update will occur in background
    ```
    kubectl apply -f dotnet-test-app/k8s/deployment-v2.yaml --record
    ```
    - Check status of pods and update
    ```
    
    ```
    - Talking points:
        - Proportional scaling during updates
        - Can pause rolling update for a canary test or use labels for canary
1. Show rollback
    - Talking points:
        - Container immutability
1. Show blue-green deployment
1. Run deployment and provision ingress for Traefik API Gateway
    - Talking points:
        - We don't want to have separate public IP's and domain names for 
        each service. Most often we want to have the services as a path 
        off a root domain
        - We can do this with an API gateway
1. Kill node to show self-healing