﻿/* http://www.zkea.net/ 
 * Copyright (c) ZKEASOFT. All rights reserved. 
 * http://www.zkea.net/licenses */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZKEACMS.PendingTask
{
    public interface IPendingTaskHandler<out T>
    {
        Task ExecuteAsync(object context);
        T Deserialize(string rowData);
    }
}