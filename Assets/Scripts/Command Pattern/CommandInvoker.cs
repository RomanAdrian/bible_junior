using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
    // ICommand delete;
    // ICommand bringFront;
    // ICommand zoomIn;
    // ICommand zoomOut;
    // ICommand reflect;

    // the invoker acts on the Element, the Invoker is the Submenu
    
    // public Invoker(delete, bringFront, zoomIn, zoomOut, reflect)
    // {
    //     this.delete = delete;
    //     this.bringFront = bringFront;
    //     this.zoomIn = zoomIn;
    //     this.zoomOut = zoomOut;
    //     this.reflect = reflect;
    // }
    
    static Queue<ICommand> commandBuffer;

    private void Awake()
    {
        commandBuffer = new Queue<ICommand>();
    }

    public static void AddCommand(ICommand command)
    {
        commandBuffer.Enqueue(command);
    }

    void Update()
    {
        if (commandBuffer.Count > 0)
        {
            ICommand c = commandBuffer.Dequeue();
            c.Execute();
            //commandBuffer.Dequeue().Execute();
        }
    }

}
