using MultiAgentWorkshop.AppHost.Extensions;

using Projects;

var builder = DistributedApplication.CreateBuilder(args);

// Add resource for Microsoft Foundry
var foundry = builder.AddFoundry("foundry");

// Add resource for agents on Microsoft Foundry
var agents = builder.AddAgents("agents");

// Add backend agent service
var agent = builder.AddProject<MultiAgentWorkshop_Agent>("agent")
                   .WithReference(foundry);

// Add frontend web UI
var webUI = builder.AddProject<MultiAgentWorkshop_WebUI>("webui")
                   .WithExternalHttpEndpoints()
                   .WithReference(agents)
                   .WithReference(agent)
                   .WaitFor(agent);

await builder.Build().RunAsync();
