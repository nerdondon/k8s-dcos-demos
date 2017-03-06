# Demo script

1. Show around the DC\OS dashboard
1. Run through instructions to set up tweeter [here](https://github.com/dcos/demos/tree/master/1.8/tweeter)
    - *Note 1: The setup should probably be done before the presentation unless you are demoing just DC/OS and tweeter*
    - *Note 2: Tweeter is included as a submodule in this repo for convenience*
1. Deploy .NET sample app
    - `dcos marathon app add dotnet-test-app/dcos/deployment.json`
1. Check that the sample app is served publicly by hitting FQDN of agent pool
    - FQDN usally in the format of: `http://<DNS PREFIX>agents.westus.cloudapp.azure.com:10002`
    - Mine was: w`http://will-starfleetagents.westus.cloudapp.azure.com:10002`
1. Turn on app that posts 100K 'tweets'
    - `dcos marathon app add azure-dcos-setup/tweeter/post-tweets.json`
1. Show Tweeter Zeppelin notebook
    - We have an open ssh tunnel so hit zeppelin at `http://localhost/service/zeppelin`
    - Talking points:
        - Discuss SMACK stack