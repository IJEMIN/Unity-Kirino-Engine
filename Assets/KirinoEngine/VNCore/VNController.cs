﻿using UnityEngine;
using System.Collections;

namespace KirinoEngine
{

    public static class VNController
    {
        private static TextDisplayer m_textDisplayer;
        public static TextDisplayer textDisplayer
        {
            get
            {
                if (!m_textDisplayer)
                {
                    m_textDisplayer = GameObject.FindObjectOfType<TextDisplayer>();
                }

                return m_textDisplayer;
            }
        }

        private static AudioManager m_audioManager;
        public static AudioManager audioManager
        {
            get
            {
                if (!m_audioManager)
                {
                    m_audioManager = GameObject.FindObjectOfType<AudioManager>();
                }

                return m_audioManager;
            }
        }




        private static DisplayableDisplayer m_displayableDisplayer;
        public static DisplayableDisplayer displayableDisplayer
        {
            get
            {
                if (!m_displayableDisplayer)
                {
                    m_displayableDisplayer = GameObject.FindObjectOfType<DisplayableDisplayer>();
                }
                return m_displayableDisplayer;
            }
        }


        private static BackgroundDisplayer m_backgroundDisplayable;
        public static BackgroundDisplayer backgroundDisplayable
        {
            get
            {
                if (!m_backgroundDisplayable)
                {
                    m_backgroundDisplayable = GameObject.FindObjectOfType<BackgroundDisplayer>();
                }
                return m_backgroundDisplayable;
            }
        }

        private static VNParser m_parser;
        public static VNParser parser
        {
            get
            {
                if (!m_parser)
                {
                    m_parser = GameObject.FindObjectOfType<VNParser>();
                }
                return m_parser;
            }
        }

        private static MenuDisplayer m_menuDisplayer;
		public static MenuDisplayer menuDisplayer
		{
			get
			{
				if (!m_menuDisplayer)
				{
					m_menuDisplayer = GameObject.FindObjectOfType<MenuDisplayer>();
				}
				return m_menuDisplayer;
			}
		}

    }

}