using System;
using System.Collections.Generic;
using System.Text;

namespace Topevery.FMP.ObjectModel
{
    /// <summary>
    /// �����ļ�����ģʽ
    /// </summary>
    public enum UpdateMode
    {
        /// <summary>
        /// ȱʡģʽ����������ֵ�����û������ֵ����Ϊ����ģʽ(Override)
        /// </summary>
        None,

        /// <summary>
        /// ÿ�θ��´����°汾
        /// </summary>
        NewVersion,

        /// <summary>
        /// ����ԭ���ļ�
        /// </summary>
        Override
    }
}
