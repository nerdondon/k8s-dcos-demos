# Demo script

1. Show around the DC\OS dashboard
1. Deploy .NET sample app
    - `dcos marathon app add dotnet-test-app/dcos/deployment.json`
1. Check that the sample app is served publicly by hitting FQDN of agent pool
    - `http://will-starfleetagents.westus.cloudapp.azure.com`
1. Run through instructions to set up tweeter [here](https://github.com/dcos/demos/tree/master/1.8/tweeter)
    - *Note 1: The setup should probably be done before the presentation unless you are demoing just DC/OS and tweeter*
    - *Note 2: Tweeter is included as a submodule in this repo for convenience*
    - Talking points:
        - Discuss SMACK stack