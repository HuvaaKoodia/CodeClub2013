using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeClubShmup1.Components;
using CodeClubShmup1.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using CodeClubShmup1.Menu;

namespace CodeClubShmup1.Scenes
{
    class HUDScene : SceneParent
    {
        string score_text = "Score 0", gameover_text = "Game Over!!!";
        Vector2 gameover_pos;
        bool gameover = false;
        int hp_sourcerectangle_width;

        public override void Start()
        {
            Vector2 size = Resources.GetFont("CSfont").MeasureString(gameover_text);
            gameover_pos = new Vector2(Game1.screen_size.Width, 
                Game1.screen_size.Height) * 0.5f - new Vector2(size.X, size.Y) * 0.5f;

            SetScore(0);
            SetPlayerHP(100);
        }

        public override void Update(float dt)
        {
            base.Update(dt);
        }

        public override void Draw()
        {
            if (gameover)
                DrawSys.DrawText(gameover_text, Resources.GetFont("CSfont"),
                    gameover_pos, Color.Red);

            DrawSys.DrawText(score_text, Resources.GetFont("CSfont"),
                new Vector2(2, 2), Color.Red);

            DrawSys.Draw(Resources.GetTexture("HP_bar_fill"),
                new Vector2(200, 2),
                new Rectangle(0, 0, hp_sourcerectangle_width, 32),
                new Color(0.5f + (1-hp_sourcerectangle_width / 128f) * 0.5f,
                    1.0f - (1-hp_sourcerectangle_width/128f)*0.8f, 0.2f,0.9f), 
                Vector2.Zero);
            DrawSys.Draw(Resources.GetTexture("HP_bar_borders"), new Vector2(200, 2));
        }

        public void SetGameOver()
        {
            gameover = true;
        }

        public void SetScore(int points)
        {
            score_text = "Score " + points;
        }

        public void SetPlayerHP(int current_hp)
        {
            hp_sourcerectangle_width = (int)(current_hp * 0.01 * 
                Resources.GetTexture("HP_bar_fill").Width);
        }
    }
}
