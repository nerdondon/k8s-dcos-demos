# Demo script

1. Show around the DC\OS dashboard
1. Deploy .NET sample app
    - `dcos marathon app add dotnet-test-app/dcos/deployment.json`
1. Check that the sample app is served publicly by hitting FQDN of agent pool
    - `http://will-starfleetagents.westus.cloudapp.azure.com`
1. Run script for installing the SMACK stack and tweeter demo app
    - Talking points:
        - Discuss SMACK stack