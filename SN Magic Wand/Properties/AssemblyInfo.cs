using System.Reflection;
using MelonLoader;

[assembly: AssemblyTitle(SecretNeighbour.BuildInfo.Description)]
[assembly: AssemblyDescription(SecretNeighbour.BuildInfo.Description)]
[assembly: AssemblyCompany(SecretNeighbour.BuildInfo.Company)]
[assembly: AssemblyProduct(SecretNeighbour.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + SecretNeighbour.BuildInfo.Author)]
[assembly: AssemblyTrademark(SecretNeighbour.BuildInfo.Company)]
[assembly: AssemblyVersion(SecretNeighbour.BuildInfo.Version)]
[assembly: AssemblyFileVersion(SecretNeighbour.BuildInfo.Version)]
[assembly: MelonInfo(typeof(SecretNeighbour.Main), SecretNeighbour.BuildInfo.Name, SecretNeighbour.BuildInfo.Version, SecretNeighbour.BuildInfo.Author, SecretNeighbour.BuildInfo.DownloadLink)]


// Create and Setup a MelonGame to mark a Mod as Universal or Compatible with specific Games.
// If no MelonGameAttribute is found or any of the Values for any MelonGame on the Mod is null or empty it will be assumed the Mod is Universal.
// Values for MelonMame can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonGame("Hologryph", "Secret Neighbour")]