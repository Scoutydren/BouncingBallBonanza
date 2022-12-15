using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    private float dimTimer;
    private float maxDimTime;

    private float undimTimer;
    private float maxUndimTime;

    private Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        this.renderer = this.GetComponent<Renderer>();
        this.renderer.material.color = Color.white;

        this.maxDimTime = 3;
        this.maxUndimTime = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.dimTimer > 0)
        {
            float interp = this.dimTimer / this.maxDimTime;
            float val = interp;
            this.renderer.material.color = new Color(val, val, val);
            this.dimTimer -= Time.deltaTime;
        } else
        {
            this.dimTimer = 0;
        }

        if (this.undimTimer > 0)
        {
            float interp = this.undimTimer / this.maxUndimTime;
            float val = 1 - interp;
            this.renderer.material.color = new Color(val, val, val);
            this.undimTimer -= Time.deltaTime;
        }
        else
        {
            this.undimTimer = 0;
        }
    }

    public void Dim()
    {
        this.dimTimer = maxDimTime;
    }
    public void UnDim()
    {
        this.undimTimer = maxUndimTime;
    }
}
