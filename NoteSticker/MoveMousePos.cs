using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NoteSticker
{
    class MoveMousePos
    {
        public static Cursor Cursor { get; private set; }

        /// <summary>
        /// "X" / "Y" (not-case-sentive) decides Which Axis The nLoc will Affect
        /// </summary>
        /// <param name="nloc"></param>
        /// <param name="AxisDir"></param>
        public static void MoveMouseLoc(int nloc, string AxisDir)
        {
            //getting new cursor
            Cursor = new Cursor(Cursor.Current.Handle);

            //Setting Cursor' Position
            switch (AxisDir.ToLower())
            {
                
                case "x":
                    {
                        Cursor.Position = new Point(Cursor.Position.X + nloc);
                    }
                    break;

                case "y":
                    {
                        Cursor.Position = new Point(Cursor.Position.Y + nloc);
                    }
                    break;
            }

        }

        
        /// <summary>
        /// Setting the Value for the new X and Y position of mouse
        /// </summary>
        /// <param name="mX"></param>
        /// <param name="mY"></param>
        public static void MoveMouseLoc(int mX, int mY)
        {
            Cursor = new Cursor(Cursor.Current.Handle);
            Cursor.Position = new Point(Cursor.Position.X + mX, Cursor.Position.Y + mY);
        }

        

        
    }
}
