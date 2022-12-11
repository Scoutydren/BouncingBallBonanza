using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedEventScript : MonoBehaviour
{

    public enum Effect
    {
        FREEZE,
        SPEED,
        NONE
    }

    public Effect currentEffect;
    public float speedMultiplier;

    private Renderer ballRenderer;
    private GlobalScript global;

    private float timer;
    private Vector3 defaultColor;

    // FREEZE
    private float freezeDuration;
    private Vector3 freezeColor;

    // SPEED
    private float speedDuration;
    private Vector3 speedColor;

    // Start is called before the first frame update
    void Start()
    {
        this. currentEffect = Effect.NONE;
        this.timer = 0;
        this.speedMultiplier = 1;
        this.freezeDuration = 7;
        this.speedDuration = 5;

        this.defaultColor = new Vector3(Color.yellow.r, Color.yellow.g, Color.yellow.b);
        this.freezeColor = new Vector3(0.5f, 0.94f, 1);
        this.speedColor = new Vector3(1, 0.6f, 0);

        this.ballRenderer = GameObject.Find("Ball").GetComponent<Renderer>();
        this.global = GameObject.Find("Global").GetComponent<GlobalScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            float interp = 1;
            Vector3 interpColor = new Vector3();

            switch (this.currentEffect)
            {
                case Effect.FREEZE:
                    interp = this.timer / this.freezeDuration;
                    interpColor = interp * this.freezeColor + (1 - interp) * this.defaultColor;
                    Debug.Log(interpColor);
                    this.ballRenderer.material.color = new Color(interpColor[0], interpColor[1], interpColor[2]);
                    break;
                case Effect.SPEED:
                    interp = this.timer / this.speedDuration;
                    interpColor = interp * this.speedColor + (1 - interp) * this.defaultColor;
                    this.ballRenderer.material.color = new Color(interpColor[0], interpColor[1], interpColor[2]);
                    break;
                case Effect.NONE:
                    break;
                default:
                    break;
            }
        }
        else
        {
            timer = 0;
            this.ClearEffects();
        }
    }

    public void AddEffect(Effect effect)
    {
        // Clear current effect
        this.ClearEffects();

        currentEffect = effect;


        switch (effect)
        {
            case Effect.FREEZE:
                this.speedMultiplier = 0.5f;
                this.timer = this.freezeDuration;
                this.global.freezeTimer = true;
                break;
            case Effect.SPEED:
                this.speedMultiplier = 1.75f;
                this.timer = this.speedDuration;
                break;
            case Effect.NONE:
                break;
            default:
                break;
        }
    }

    public void ClearEffects()
    {
        this.global.freezeTimer = false;
        this.currentEffect = Effect.NONE;
        this.speedMultiplier = 1;
        this.ballRenderer.material.color = Color.yellow;
    }
}
