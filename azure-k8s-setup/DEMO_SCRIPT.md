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

1. Show scaling
    - *Note: Autoscaling will be shown later becuase there is more setup*
    - Scale imperatively:
        ```
        kubectl scale deployment dotnet-test-app --replicas 7
        kubectl get deployment
        ```
        or declaratively by changing manifest and `kubectl apply`

1. Show rolling updates
    - Simply apply changes in the manifest from version control and update will occur in background
    ```
    kubectl apply -f dotnet-test-app/k8s/deployment-v2.yaml --record
    ```
    - Check status of pods and update
    ```
    kubectl rollout status deployment/dotnet-test-app
    kubectl get deployments
    ```
    - Show unbroken connection with below just as another source of confirmation:
    ```
    kubectl run curl --image=radial/busyboxplus:curl -i --tty
    // While in pod prompt
    while true; do curl -o /dev/null --silent --head --write-out '%{http_code}\n' dotnet-test-app; sleep 2; done
    ```
    - Talking points:
        - Proportional scaling during updates
        - Use labels for canary tests

1. Show rollback
    - Rollback to v1
    ```
    kubectl rollout history deployment/dotnet-test-app
    kubectl rollout undo deployment/dotnet-test-app
    // OR
    kubectl rollout undo deployment/dotnet-test-app --to-revision=<#num>
    ```
    - Put it at v3 now
    ```
    kubectl apply -f dotnet-test-app/k8s/deployment-v3.yaml --record
    ```
    - Talking points:
        - Container immutability

1. Show blue-green deployment **- resume testing here**
    - Create resources
    ```
    kubectl create -f dotnet-test-app/k8s/blue-green/deployment-v1.yaml --record --save-config
    kubectl create -f dotnet-test-app/k8s/blue-green/service.yaml --record --save-config    
    ```
    - Show that v1 (blue) is being served
    - Create version 2 deployment
    ```
    kubectl create -f dotnet-test-app/k8s/blue-green/deployment-v2.yaml --record --save-config
    ```
    - Change service label `kubectl label svc dotnet-test-app-bg color=green`
    - Show that v2 (green) is being served
    - Switch back label and show that it is v1 (blue) again

1. Run deployment and provision ingress for Traefik API Gateway
    - Run deployment
    ```
    kubectl create -f api-gateway/traefik/deployment.yaml --record --save-config
    kubectl create -f api-gateway/traefik/path-based-ingress.yaml --record --save-config
    ```
    - Show ingress created in dashboard
    - Hit endpoints via paths
    - Talking points:
        - We don't want to have separate public IP's and domain names for 
        each service. Most often we want to have the services as a path 
        off a root domain. We can do this and lots more with an API gateway.
        
1. Kill node to show self-healing

1. Show autoscaling
    - Deploy slow version of dotnet-test-app
    ```
    kubectl apply -f dotnet-test-app/k8s/deployment-slow.yaml --record
    ```
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