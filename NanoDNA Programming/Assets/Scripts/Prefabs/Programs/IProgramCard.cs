using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProgramCard 
{
    public void setAction();

    public ProgramAction createAction();

    public void setInfo();

}
