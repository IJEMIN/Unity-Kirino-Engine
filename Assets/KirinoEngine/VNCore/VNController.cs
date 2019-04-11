using UnityEngine;

namespace KirinoEngine {
    public static class VNController {

        private static TextDisplayer m_textDisplayer;

        private static AudioManager m_audioManager;


        private static DisplayableDisplayer m_displayableDisplayer;


        private static BackgroundDisplayer m_backgroundDisplayer;


        private static MenuDisplayer m_menuDisplayer;


        public static TextDisplayer textDisplayer
        {
            get
            {
                if (!m_textDisplayer) m_textDisplayer = Object.FindObjectOfType<TextDisplayer>();

                return m_textDisplayer;
            }
        }

        public static AudioManager audioManager
        {
            get
            {
                if (!m_audioManager) m_audioManager = Object.FindObjectOfType<AudioManager>();

                return m_audioManager;
            }
        }

        public static DisplayableDisplayer displayableDisplayer
        {
            get
            {
                if (!m_displayableDisplayer) m_displayableDisplayer = Object.FindObjectOfType<DisplayableDisplayer>();
                return m_displayableDisplayer;
            }
        }

        public static BackgroundDisplayer backgroundDisplayer
        {
            get
            {
                if (!m_backgroundDisplayer) m_backgroundDisplayer = Object.FindObjectOfType<BackgroundDisplayer>();
                return m_backgroundDisplayer;
            }
        }


        public static MenuDisplayer menuDisplayer
        {
            get
            {
                if (!m_menuDisplayer) m_menuDisplayer = Object.FindObjectOfType<MenuDisplayer>();
                return m_menuDisplayer;
            }
        }


    }
}