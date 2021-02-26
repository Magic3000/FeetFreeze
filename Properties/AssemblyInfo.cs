using System.Resources;
using System.Reflection;
using System.Runtime.InteropServices;
using MelonLoader;

[assembly: AssemblyTitle(FeetFreeze.BuildInfo.Name)]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(FeetFreeze.BuildInfo.Company)]
[assembly: AssemblyProduct(FeetFreeze.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + FeetFreeze.BuildInfo.Author)]
[assembly: AssemblyTrademark(FeetFreeze.BuildInfo.Company)]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
//[assembly: Guid("")]
[assembly: AssemblyVersion(FeetFreeze.BuildInfo.Version)]
[assembly: AssemblyFileVersion(FeetFreeze.BuildInfo.Version)]
[assembly: NeutralResourcesLanguage("en")]
[assembly: MelonInfo(typeof(FeetFreeze.FeetFreezeMain), FeetFreeze.BuildInfo.Name, FeetFreeze.BuildInfo.Version, FeetFreeze.BuildInfo.Author, FeetFreeze.BuildInfo.DownloadLink)]


// Create and Setup a MelonModGame to mark a Mod as Universal or Compatible with specific Games.
// If no MelonModGameAttribute is found or any of the Values for any MelonModGame on the Mod is null or empty it will be assumed the Mod is Universal.
// Values for MelonModGame can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonGame(null, null)]