using CommandsParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandsParserSample.Commands
{
    public class TextCommand:BaseCommand
    {
        private string[] textLines = new string[]
        {

            "Lorem ipsum dolor sit, amet consectetur adipiscing, elit eros.",
            "Aenean faucibus congue eu vel lectus pretium, ultrices tempor mattis nec.",
            "Scelerisque sociosqu sem curabitur egestas, habitasse curae.",
            "Ullamcorper sodales cum accumsan egestas scelerisque, congue libero quam nullam.",
            "Magna congue lacinia netus dapibus non, lectus quis accumsan.",
            "Laoreet lobortis ornare facilisis nascetur id, tempor venenatis egestas.",
            "Semper netus justo posuere mattis, dictum et velit.",
            "Phasellus sed ad vestibulum suscipit diam, hendrerit lacus natoque.",
            "Placerat donec feugiat sociosqu eleifend mollis natoque, vivamus libero nec litora.",
            "Diam cras ante lobortis id suscipit, congue cubilia litora.",
            "Magnis feugiat commodo tellus blandit inceptos, quis auctor massa.",
            "Pellentesque urna torquent felis, morbi integer maecenas, mus ultricies.",
            "Eu montes pulvinar congue, quam malesuada at phasellus, platea ad.",
            "Et per justo auctor lacinia, natoque at conubia.",
            "Fames metus fringilla imperdiet etiam, semper conubia natoque.",
            "Ante erat viverra feugiat inceptos bibendum, fusce ridiculus lobortis cubilia.",
            "Lacinia tempus quam posuere nascetur, mus nulla.",
            "Nascetur mauris netus risus inceptos, ut libero.",
            "Elementum porta ut eget dictumst, per sociis.",
            "Fringilla felis ligula mus fusce, nostra ridiculus malesuada.",
            "Vitae lectus justo quisque, iaculis habitant turpis odio, ullamcorper torquent.",
            "Nostra habitant elementum magnis id, risus pellentesque orci.",
            "Auctor nullam posuere natoque sollicitudin nec, class eleifend eget.",
            "Pharetra interdum phasellus ac metus, tristique natoque.",
            "Malesuada ad egestas suspendisse, eros vivamus rutrum erat, cubilia ligula.",
            "Inceptos nascetur viverra turpis donec potenti, rutrum risus pellentesque class.",
            "Potenti rutrum duis torquent ut parturient, gravida et sem.",
            "Duis tempus morbi ultrices leo, hendrerit ut vehicula.",
            "Condimentum proin facilisi at in molestie, leo quis mollis egestas.",
            "Ornare nam quis augue, lacus sem vulputate, sociis interdum.",
            "Rutrum nisi vehicula venenatis in, torquent elementum.",
            "Nulla litora justo semper vel sodales, euismod eleifend faucibus ridiculus."
        };
        private Random rnd;

        public TextCommand(CmdParser cmdParser)
           : base(cmdParser,
                 "text",
                 "Echoes a series of text lines",
                 new List<string>() { "cls" },
                 null)
        {
            rnd = new Random();
            IsMuted = true;
        }

        public override string[] Help
        {
            get
            {
                return new string[] {
                    "Echoes a series of text lines"};
            }
        }

        public override void Execute(string[] arguments)
        {
            if (arguments.Length == 0)
                AppendOutputLine(GetRandomString());
            else
            {
                int lineCount = int.Parse(arguments[0]);
                for (int i = 0; i < lineCount; i++)
                {
                    AppendOutputLine(GetRandomString());
                }
            }
            OnCommandExecuted(arguments);
        }

        private string GetRandomString()
        {
            int rndIndex = rnd.Next(0, textLines.Length);

            return textLines[rndIndex];
        }
    }
}
