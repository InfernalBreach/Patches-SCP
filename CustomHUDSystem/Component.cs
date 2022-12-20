using System;
using System.Collections.Generic;
using System.Linq;
using PlayerRoles;
using PluginAPI.Core;
using UnityEngine;
using InfernalExtensions.Extensions;

namespace CustomHUDSystem
{
    public class Component : MonoBehaviour
    {
        public Player player { get; private set; }

        private string _hudTemplate = "<line-height=95%><voffset=8.5em><align=left><size=50%><alpha=#44>[STATS]<alpha=#ff></size></align>\n<align=right>[LIST]</align><align=center>[CENTER_UP][CENTER][CENTER_DOWN][BOTTOM]";
        private float _timer = 0f;
        private Tip _proTip;
        private int _timerCount = 0;
        private string _hudText = string.Empty;
		
        private string _hudCenterUpString = string.Empty;
        private float _hudCenterUpTime = -1f;
        private float _hudCenterUpTimer = 0f;
		
        private string _hudCenterString = string.Empty;
        private float _hudCenterTime = -1f;
        private float _hudCenterTimer = 0f;
		
        private string _hudCenterDownString = string.Empty;
        private float _hudCenterDownTime = -1f;
        private float _hudCenterDownTimer = 0f;
		
        public static List<Component> Instances = new List<Component>();

        private void Start()
        {
            player = Player.Get<Player>(gameObject);
            Instances.Add(this);
        }
        private void FixedUpdate()
        {
            _timer += Time.deltaTime;

            UpdateTimers();

            if(_timer > 0.5f)
            {
                UpdateExHud();
				
                _timer = 0f;
            }				
        }
        public void AddHudCenterUpText(string text, ulong timer)
        {
            _hudCenterUpString = text;
            _hudCenterUpTime = timer;
            _hudCenterUpTimer = 0f;
        }

        public void ClearHudCenterUpText()
        {
            _hudCenterTime = -1f;
        }
		
        public void AddHudCenterText(string text, ulong timer)
        {
            _hudCenterString = text;
            _hudCenterTime = timer;
            _hudCenterTimer = 0f;
        }

        public void ClearHudCenterText()
        {
            _hudCenterTime = -1f;
        }
		
        public void AddHudCenterDownText(string text, ulong timer)
        {
            _hudCenterDownString = text;
            _hudCenterDownTime = timer;
            _hudCenterDownTimer = 0f;
        }

        public void ClearHudCenterDownText()
        {
            _hudCenterDownTime = -1f;
        }
        public void UpdateTimers()
        {
            if (_hudCenterUpTimer < _hudCenterUpTime)
                _hudCenterUpTimer += Time.deltaTime;
            else
                _hudCenterUpString = string.Empty;
			
            if (_hudCenterTimer < _hudCenterTime)
                _hudCenterTimer += Time.deltaTime;
            else
                _hudCenterString = string.Empty;
			
            if(_hudCenterDownTimer < _hudCenterDownTime)
                _hudCenterDownTimer += Time.deltaTime;
            else
                _hudCenterDownString = string.Empty;
        }

        private string FormatStringForHud(string text, int needNewLine)
        {
            int curNewLine = text.Count(x => x == '\n');
            for(int i = 0; i < needNewLine - curNewLine; i++)
                text += '\n';
            return text;
        }

        private void UpdateExHud()
        {
            string curHud = string.Empty;

            if (GamemodeCore.GamemodeCore.Instance.CurrentEvent is null)
                curHud = _hudTemplate.Replace("[STATS]",
                    $"<color=yellow>★ </color><color=#db1b1bff>I</color><color=#dc211bff>n</color><color=#dd271aff>f</color><color=#dd2d1aff>e</color><color=#de321aff>r</color><color=#df3819ff>n</color><color=#e03e19ff>a</color><color=#e04419ff>l</color><color=#e14a19ff>B</color><color=#e25018ff>r</color><color=#e35518ff>e</color><color=#e35b18ff>a</color><color=#e46117ff>c</color><color=#e56717ff>h</color><color=yellow> ★</color> [Hora: {DateTime.UtcNow.AddHours(1):HH:mm:ss}] - [Ronda: {RoundExtensions.ElapsedTime.Minutes}min / {RoundExtensions.ElapsedTime.Seconds}s]");
            else
            {
                curHud = _hudTemplate.Replace("[STATS]",
                    $"<color=yellow>★ </color><color=#db1b1bff>I</color><color=#dc211bff>n</color><color=#dd271aff>f</color><color=#dd2d1aff>e</color><color=#de321aff>r</color><color=#df3819ff>n</color><color=#e03e19ff>a</color><color=#e04419ff>l</color><color=#e14a19ff>B</color><color=#e25018ff>r</color><color=#e35518ff>e</color><color=#e35b18ff>a</color><color=#e46117ff>c</color><color=#e56717ff>h</color><color=yellow> ★</color> [Hora: {DateTime.UtcNow.AddHours(1):HH:mm:ss}] - [Ronda: {RoundExtensions.ElapsedTime.Minutes}min / {RoundExtensions.ElapsedTime.Seconds}s] - [Evento: {GamemodeCore.GamemodeCore.Instance.CurrentEvent?.EventName}]");
            }

            curHud = curHud.Replace("[LIST]", FormatStringForHud(string.Empty, 6));

            if(!string.IsNullOrEmpty(_hudCenterUpString))
                curHud = curHud.Replace("[CENTER_UP]", FormatStringForHud(_hudCenterUpString, 6));
            else
                curHud = curHud.Replace("[CENTER_UP]", FormatStringForHud(string.Empty, 6));

            if(!string.IsNullOrEmpty(_hudCenterString))
                curHud = curHud.Replace("[CENTER]", FormatStringForHud(_hudCenterString, 6));
            else
                curHud = curHud.Replace("[CENTER]", FormatStringForHud(string.Empty, 6));

            if(!string.IsNullOrEmpty(_hudCenterDownString))
                curHud = curHud.Replace("[CENTER_DOWN]", FormatStringForHud(_hudCenterDownString, 7));
            else
                curHud = curHud.Replace("[CENTER_DOWN]", FormatStringForHud(string.Empty, 7));

            if (player.IsAlive || !Round.IsRoundStarted)
            {
                curHud = curHud.Replace("[BOTTOM]", "　");

                _proTip = null;
                _timerCount = 0;
            }

            if (player.Role is RoleTypeId.Spectator)
            {
                if (_proTip == null)
                {
                    _proTip = Extensions.Tips.PickRandom();
                }
				
                _timerCount++;
                if (_timerCount != 20)
                {
                    curHud = curHud.Replace("[BOTTOM]", _proTip.Message);
                }
                else
                {
                    _proTip = Extensions.Tips.PickRandom();
                    curHud = curHud.Replace("[BOTTOM]", _proTip.Message);
                    _timerCount = 0;
                }
            }
            _hudText = curHud;
            player.SendTextHintNotEffect(_hudText, 1);
        }
    }
}