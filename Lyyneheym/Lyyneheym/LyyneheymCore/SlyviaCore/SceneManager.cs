﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lyyneheym.LyyneheymCore.SlyviaCore
{
    /// <summary>
    /// <para>场景管理器：负责控制整个剧本的演绎过程</para>
    /// <para>她是一个单例类，只有唯一实例</para>
    /// </summary>
    public class SceneManager
    {

        public static SceneManager getInstance()
        {
            return null == synObject ? synObject = new SceneManager() : synObject;
        }

        private SceneManager()
        {

        }

        private static SceneManager synObject = null;
    }
}