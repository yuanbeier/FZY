using System;
using System.Collections.Generic;
using System.Text;

namespace Topevery.FMP.ObjectModel
{
    /// <summary>
    /// ���ݲ���״̬
    /// </summary>
    [Flags]
    public enum DataAction
    {
        /// <summary>
        /// ����
        /// </summary>
        None = 0,
        
        /// <summary>
        /// ɾ��
        /// </summary>
        Delete = 1,
        
        /// <summary>
        /// �޸�
        /// </summary>
        Change = 2,
        
        /// <summary>
        /// ���
        /// </summary>
        Add    = 4,
        
        
        /// <summary>
        /// �ύ
        /// </summary>
        Commit = 8,
        
        /// <summary>
        /// �ع�
        /// </summary>
        Rollback = 16,
        
        /// <summary>
        /// δ֪
        /// </summary>
        Unknown = 2 ^ 30
    }
}
