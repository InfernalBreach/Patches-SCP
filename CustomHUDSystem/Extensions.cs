using System.Collections.Generic;
using Hints;
using PluginAPI.Core;

namespace CustomHUDSystem
{
    public static class Extensions
    {
        public static T PickRandom<T>(this List<T> List)
        {
            return List[MainClass.random.Next(List.Count)];
        }
        public static void SendTextHintNotEffect(this Player player, string text, float time)
        {
            player.ReferenceHub.hints.Show(new TextHint(text, new HintParameter[] { new StringHintParameter(string.Empty) }, null, time));
        }
        public static void ShowCenterHint(this Player player, string text, ulong time = 1)
        {
            player.GameObject.GetComponent<Component>().AddHudCenterText(text, time);
        }
        public static void ShowCenterUpHint(this Player player, string text, ulong time = 1)
        {
            player.GameObject.GetComponent<Component>().AddHudCenterUpText(text, time);
        }
        public static void ShowCenterDownHint(this Player player, string text, ulong time = 1)
        {
            player.GameObject.GetComponent<Component>().AddHudCenterDownText(text, time);
        }
        public static List<string> nukeMsgs = new List<string> 
        {
            "FACILITY CONNECTION ERROR 0x012fc Chaos Insurgency Interference",
            "FACILIT& CO3NECTION ERROR 0xeefad Ch!aos Infurg3ncy Int3rfEr%nce",
            "FHCIL^TY CONNeCTION ERROR 0x0101a Cha#os InAurgEncy Int3rfEreAence",
            "FAC!LITY @ONNECT!ON ERROR 0x0871c Cha6s Insurgency Interf3ren$%ce",
            "FACILITY CONNECT!ON ERROR 0xcacec ChAos IAeurg#ncy InterfEr3nc3",
            "fACIL1TY C0NNECTIoN ERROR 0xaaaaa CHaOs Ifsurg!ncy Interf3r3nc3",
            "HACIL0TY C0NNECTION ERROR 0xafed1 ChAoS I!nsuFrg2@ncy &I@nt3rferenc3",
        };
        public static List<Tip> Tips = new List<Tip>
        {
            new Tip("<color=#ff5aa4>TIP:</color> No le mires la cara al <color=red>SCP-096</color>", 5),
            new Tip("<color=#ff5aa4>TIP:</color> Si tiras objetos en la sala del <color=red>SCP-173</color> puedes obtener nuevos objetos", 5),
            new Tip("<color=#ff5aa4>TIP:</color> Si andas con la C el <color=red>SCP-939</color> no puede verte a no ser que le toques", 5),
            new Tip("<color=#ff5aa4>TIP:</color> No dejes de mirar al <color=red>SCP-173</color> si no quieres morir", 5),
            new Tip("<color=#ff5aa4>TIP:</color> Si vas al centro de los agujeros de LCZ, seras absorbido por el", 5),
            new Tip("<color=#ff5aa4>TIP:</color> El <color=#b2dafa>Cazador MTF</color> puede ver a traves de las paredes con su Vision Infraroja", 5),
            new Tip("<color=#ff5aa4>TIP:</color> Al tomarte el <color=red>SCP-109</color> seras potenciado con 200HP", 5),
            new Tip("<color=#ff5aa4>TIP:</color> La Tranquilizadora tranquiliza a todos menos al <color=red>SCP-173</color>", 5)
        };
    }
    public class Tip
    {
        public int Time { get; }
        public string Message { get; }

        public Tip(string message, int time)
        {
            Time = time;
            Message = message + "\n";
        }
    }
}