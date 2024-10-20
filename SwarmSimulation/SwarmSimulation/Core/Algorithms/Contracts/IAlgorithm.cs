using System.Numerics;
using SwarmSimulation.Core.Agents;
using SwarmSimulation.Core.Agents.Implementation;

namespace SwarmSimulation.Core.Algorithms.Contracts
{
    public interface IAlgorithm<TSettings, in TInput>
    {
        TSettings Settings { get; set; }
        void ConfigureSettings(TSettings settings);
        Vector2 CalculateControlInput(RegularAgent agent, TInput input);
    }
}