// Custom Action by DumbGameDev
// www.dumbgamedev.com

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Path")]
    [Tooltip("Change line renderer gradient colors.")]

	public class setLineRendererGradient : FsmStateAction
	{
        // Playmaker variables

        [RequiredField]
		[CheckForComponent(typeof(LineRenderer))]
		[Tooltip("Gameobject holding the line renderer.")]
		public FsmOwnerDefault gameObject;

        [ActionSection("First Color")]

        public FsmColor firstColor;

        [HasFloatSlider(0, 1)]
        public FsmFloat firstColorPosition;

        [HasFloatSlider(0, 1)]
        public FsmFloat firstAlpha;

        [HasFloatSlider(0, 1)]
        public FsmFloat firstAlphaPosition;

        [ActionSection("Second Color")]

        public FsmColor secondColor;

        [HasFloatSlider(0, 1)]
        public FsmFloat secondColorPosition;

        [HasFloatSlider(0, 1)]
        public FsmFloat secondAlpha;

        [HasFloatSlider(0, 1)]
        public FsmFloat secondAlphaPosition;

        public FsmBool everyFrame;

        // private variables

        private LineRenderer lr;

        public override void Reset()
		{

            firstColor = null;
            secondColor = null;
            firstColorPosition = 0f;
            secondColorPosition = 1f;
            firstAlphaPosition = 0f;
            secondAlphaPosition = 1f;
            firstAlpha = 1f;
            secondAlpha = 1f;
            gameObject = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
            lr = go.GetComponent<LineRenderer>();


            if (!everyFrame.Value)
			{
				DoColorChange();
				Finish();
			}

        }

        public override void OnUpdate()
		{
			if (everyFrame.Value)
			{
                DoColorChange();
			}
		}

		void DoColorChange()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}

            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(firstColor.Value, firstColorPosition.Value), new GradientColorKey(secondColor.Value, secondColorPosition.Value) },
                new GradientAlphaKey[] { new GradientAlphaKey(firstAlpha.Value, firstAlphaPosition.Value), new GradientAlphaKey(secondAlpha.Value, secondAlphaPosition.Value) }
                );
            lr.colorGradient = gradient;
        }

	}
}