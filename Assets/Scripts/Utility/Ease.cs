/**
 * PennerDoubleAnimation
 * Animates the value of a double property between two target values using 
 * Robert Penner's easing equations for interpolation over a specified Duration.
 *
 * @author		Darren David darren-code@lookorfeel.com
 * @version		1.0
 *
 * Credit/Thanks:
 * Robert Penner - The easing equations we all know and love 
 *   (http://robertpenner.com/easing/) [See License.txt for license info]
 * 
 * Lee Brimelow - initial port of Penner's equations to WPF 
 *   (http://thewpfblog.com/?p=12)
 * 
 * Zeh Fernando - additional equations (out/in) from 
 *   caurina.transitions.Tweener (http://code.google.com/p/tweener/)
 *   [See License.txt for license info]
 */

using UnityEngine;
using System;

public class Easing
{
    /// <summary>
    /// Enumeration of all easing equations.
    /// </summary>
    public enum EaseType
    {
        Linear,
        QuadEaseOut, QuadEaseIn, QuadEaseInOut, QuadEaseOutIn,
        ExpoEaseOut, ExpoEaseIn, ExpoEaseInOut, ExpoEaseOutIn,
        CubicEaseOut, CubicEaseIn, CubicEaseInOut, CubicEaseOutIn,
        QuartEaseOut, QuartEaseIn, QuartEaseInOut, QuartEaseOutIn,
        QuintEaseOut, QuintEaseIn, QuintEaseInOut, QuintEaseOutIn,
        CircEaseOut, CircEaseIn, CircEaseInOut, CircEaseOutIn,
        SineEaseOut, SineEaseIn, SineEaseInOut, SineEaseOutIn,
        ElasticEaseOut, ElasticEaseIn, ElasticEaseInOut, ElasticEaseOutIn,
        BounceEaseOut, BounceEaseIn, BounceEaseInOut, BounceEaseOutIn,
        BackEaseOut, BackEaseIn, BackEaseInOut, BackEaseOutIn,
		Curve,
    }

	public static double Ease(double CurrentTime, double StartValue, double FinalValueDifference, double Duration, EaseType eType)
	{
		return Ease(CurrentTime, StartValue, FinalValueDifference, Duration, eType, 0.0f);
	}

	public static double Ease(double CurrentTime, double StartValue, double FinalValueDifference, double Duration, EaseType eType, double CurveOffset)
	{
		switch(eType)
		{
		case EaseType.Linear: return Linear(CurrentTime, StartValue, FinalValueDifference, Duration);
	    case EaseType.QuadEaseOut: return QuadEaseOut(CurrentTime, StartValue, FinalValueDifference, Duration); 
		case EaseType.QuadEaseIn: return QuadEaseIn(CurrentTime, StartValue, FinalValueDifference, Duration); 
		case EaseType.QuadEaseInOut: return QuadEaseInOut(CurrentTime, StartValue, FinalValueDifference, Duration); 
		case EaseType.QuadEaseOutIn: return QuartEaseOutIn(CurrentTime, StartValue, FinalValueDifference, Duration);
	    case EaseType.ExpoEaseOut: return ExpoEaseOut(CurrentTime, StartValue, FinalValueDifference, Duration); 
		case EaseType.ExpoEaseIn: return ExpoEaseIn(CurrentTime, StartValue, FinalValueDifference, Duration); 
		case EaseType.ExpoEaseInOut: return ExpoEaseInOut(CurrentTime, StartValue, FinalValueDifference, Duration); 
		case EaseType.ExpoEaseOutIn: return ExpoEaseOutIn(CurrentTime, StartValue, FinalValueDifference, Duration);
        case EaseType.CubicEaseOut: return CubicEaseOut(CurrentTime, StartValue, FinalValueDifference, Duration); 
		case EaseType.CubicEaseIn: return CubicEaseIn(CurrentTime, StartValue, FinalValueDifference, Duration); 
		case EaseType.CubicEaseInOut: return CubicEaseInOut(CurrentTime, StartValue, FinalValueDifference, Duration); 
		case EaseType.CubicEaseOutIn: return CubicEaseOutIn(CurrentTime, StartValue, FinalValueDifference, Duration);
        case EaseType.QuartEaseOut: return QuartEaseOut(CurrentTime, StartValue, FinalValueDifference, Duration); 
		case EaseType.QuartEaseIn: return QuartEaseIn(CurrentTime, StartValue, FinalValueDifference, Duration);
		case EaseType.QuartEaseInOut: return QuartEaseInOut(CurrentTime, StartValue, FinalValueDifference, Duration); 
		case EaseType.QuartEaseOutIn: return QuartEaseOutIn(CurrentTime, StartValue, FinalValueDifference, Duration);
        case EaseType.QuintEaseOut: return QuintEaseOut(CurrentTime, StartValue, FinalValueDifference, Duration); 
		case EaseType.QuintEaseIn: return QuintEaseIn(CurrentTime, StartValue, FinalValueDifference, Duration); 
		case EaseType.QuintEaseInOut: return QuintEaseInOut(CurrentTime, StartValue, FinalValueDifference, Duration); 
		case EaseType.QuintEaseOutIn: return QuintEaseOutIn(CurrentTime, StartValue, FinalValueDifference, Duration);
        case EaseType.CircEaseOut: return CircEaseOut(CurrentTime, StartValue, FinalValueDifference, Duration); 
		case EaseType.CircEaseIn: return CircEaseIn(CurrentTime, StartValue, FinalValueDifference, Duration); 
		case EaseType.CircEaseInOut: return CircEaseInOut(CurrentTime, StartValue, FinalValueDifference, Duration); 
		case EaseType.CircEaseOutIn: return CircEaseOutIn(CurrentTime, StartValue, FinalValueDifference, Duration);
        case EaseType.SineEaseOut: return SineEaseOut(CurrentTime, StartValue, FinalValueDifference, Duration); 
		case EaseType.SineEaseIn: return SineEaseIn(CurrentTime, StartValue, FinalValueDifference, Duration); 
		case EaseType.SineEaseInOut: return SineEaseInOut(CurrentTime, StartValue, FinalValueDifference, Duration); 
		case EaseType.SineEaseOutIn: return SineEaseOutIn(CurrentTime, StartValue, FinalValueDifference, Duration);
        case EaseType.ElasticEaseOut: return ElasticEaseOut(CurrentTime, StartValue, FinalValueDifference, Duration); 
		case EaseType.ElasticEaseIn: return ElasticEaseIn(CurrentTime, StartValue, FinalValueDifference, Duration); 
		case EaseType.ElasticEaseInOut: return ElasticEaseInOut(CurrentTime, StartValue, FinalValueDifference, Duration); 
		case EaseType.ElasticEaseOutIn: return ElasticEaseOutIn(CurrentTime, StartValue, FinalValueDifference, Duration);
        case EaseType.BounceEaseOut: return BounceEaseOut(CurrentTime, StartValue, FinalValueDifference, Duration); 
		case EaseType.BounceEaseIn: return BounceEaseIn(CurrentTime, StartValue, FinalValueDifference, Duration); 
		case EaseType.BounceEaseInOut: return BounceEaseInOut(CurrentTime, StartValue, FinalValueDifference, Duration); 
		case EaseType.BounceEaseOutIn: return BounceEaseOutIn(CurrentTime, StartValue, FinalValueDifference, Duration);
        case EaseType.BackEaseOut: return BackEaseOut(CurrentTime, StartValue, FinalValueDifference, Duration); 
		case EaseType.BackEaseIn: return BackEaseIn(CurrentTime, StartValue, FinalValueDifference, Duration); 
		case EaseType.BackEaseInOut: return BackEaseInOut(CurrentTime, StartValue, FinalValueDifference, Duration); 
		case EaseType.BackEaseOutIn: return BackEaseOutIn(CurrentTime, StartValue, FinalValueDifference, Duration);
		case EaseType.Curve: return Curve(CurrentTime, StartValue, FinalValueDifference, Duration, CurveOffset);
		default: return Linear(CurrentTime, StartValue, FinalValueDifference, Duration);
		}
		
	}


    /// <summary>
    /// Easing equation function for a simple linear tweening, with no easing.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double Linear( double t, double b, double c, double d )
    {
        return c * t / d + b;
    }
    /// <summary>
    /// Easing equation function for an exponential (2^t) easing out: 
    /// decelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double ExpoEaseOut( double t, double b, double c, double d )
    {
        return ( t == d ) ? b + c : c * ( -Math.Pow( 2, -10 * t / d ) + 1 ) + b;
    }

    /// <summary>
    /// Easing equation function for an exponential (2^t) easing in: 
    /// accelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double ExpoEaseIn( double t, double b, double c, double d )
    {
        return ( t == 0 ) ? b : c * Math.Pow( 2, 10 * ( t / d - 1 ) ) + b;
    }

    /// <summary>
    /// Easing equation function for an exponential (2^t) easing in/out: 
    /// acceleration until halfway, then deceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double ExpoEaseInOut( double t, double b, double c, double d )
    {
        if ( t == 0 )
            return b;

        if ( t == d )
            return b + c;

        if ( ( t /= d / 2 ) < 1 )
            return c / 2 * Math.Pow( 2, 10 * ( t - 1 ) ) + b;

        return c / 2 * ( -Math.Pow( 2, -10 * --t ) + 2 ) + b;
    }

    /// <summary>
    /// Easing equation function for an exponential (2^t) easing out/in: 
    /// deceleration until halfway, then acceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double ExpoEaseOutIn( double t, double b, double c, double d )
    {
        if ( t < d / 2 )
            return ExpoEaseOut( t * 2, b, c / 2, d );

        return ExpoEaseIn( ( t * 2 ) - d, b + c / 2, c / 2, d );
    }

    /// <summary>
    /// Easing equation function for a circular (sqrt(1-t^2)) easing out: 
    /// decelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double CircEaseOut( double t, double b, double c, double d )
    {
        return c * Math.Sqrt( 1 - ( t = t / d - 1 ) * t ) + b;
    }

    /// <summary>
    /// Easing equation function for a circular (sqrt(1-t^2)) easing in: 
    /// accelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double CircEaseIn( double t, double b, double c, double d )
    {
        return -c * ( Math.Sqrt( 1 - ( t /= d ) * t ) - 1 ) + b;
    }

    /// <summary>
    /// Easing equation function for a circular (sqrt(1-t^2)) easing in/out: 
    /// acceleration until halfway, then deceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double CircEaseInOut( double t, double b, double c, double d )
    {
        if ( ( t /= d / 2 ) < 1 )
            return -c / 2 * ( Math.Sqrt( 1 - t * t ) - 1 ) + b;

        return c / 2 * ( Math.Sqrt( 1 - ( t -= 2 ) * t ) + 1 ) + b;
    }

    /// <summary>
    /// Easing equation function for a circular (sqrt(1-t^2)) easing in/out: 
    /// acceleration until halfway, then deceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double CircEaseOutIn( double t, double b, double c, double d )
    {
        if ( t < d / 2 )
            return CircEaseOut( t * 2, b, c / 2, d );

        return CircEaseIn( ( t * 2 ) - d, b + c / 2, c / 2, d );
    }
    /// <summary>
    /// Easing equation function for a quadratic (t^2) easing out: 
    /// decelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double QuadEaseOut( double t, double b, double c, double d )
    {
        return -c * ( t /= d ) * ( t - 2 ) + b;
    }

    /// <summary>
    /// Easing equation function for a quadratic (t^2) easing in: 
    /// accelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double QuadEaseIn( double t, double b, double c, double d )
    {
        return c * ( t /= d ) * t + b;
    }

    /// <summary>
    /// Easing equation function for a quadratic (t^2) easing in/out: 
    /// acceleration until halfway, then deceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double QuadEaseInOut( double t, double b, double c, double d )
    {
        if ( ( t /= d / 2 ) < 1 )
            return c / 2 * t * t + b;

        return -c / 2 * ( ( --t ) * ( t - 2 ) - 1 ) + b;
    }

    /// <summary>
    /// Easing equation function for a quadratic (t^2) easing out/in: 
    /// deceleration until halfway, then acceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double QuadEaseOutIn( double t, double b, double c, double d )
    {
        if ( t < d / 2 )
            return QuadEaseOut( t * 2, b, c / 2, d );

        return QuadEaseIn( ( t * 2 ) - d, b + c / 2, c / 2, d );
    }
    /// <summary>
    /// Easing equation function for a sinusoidal (sin(t)) easing out: 
    /// decelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double SineEaseOut( double t, double b, double c, double d )
    {
        return c * Math.Sin( t / d * ( Math.PI / 2 ) ) + b;
    }

    /// <summary>
    /// Easing equation function for a sinusoidal (sin(t)) easing in: 
    /// accelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double SineEaseIn( double t, double b, double c, double d )
    {
        return -c * Math.Cos( t / d * ( Math.PI / 2 ) ) + c + b;
    }

    /// <summary>
    /// Easing equation function for a sinusoidal (sin(t)) easing in/out: 
    /// acceleration until halfway, then deceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double SineEaseInOut( double t, double b, double c, double d )
    {
        if ( ( t /= d / 2 ) < 1 )
            return c / 2 * ( Math.Sin( Math.PI * t / 2 ) ) + b;

        return -c / 2 * ( Math.Cos( Math.PI * --t / 2 ) - 2 ) + b;
    }

    /// <summary>
    /// Easing equation function for a sinusoidal (sin(t)) easing in/out: 
    /// deceleration until halfway, then acceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double SineEaseOutIn( double t, double b, double c, double d )
    {
        if ( t < d / 2 )
            return SineEaseOut( t * 2, b, c / 2, d );

        return SineEaseIn( ( t * 2 ) - d, b + c / 2, c / 2, d );
    }
    /// <summary>
    /// Easing equation function for a cubic (t^3) easing out: 
    /// decelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double CubicEaseOut( double t, double b, double c, double d )
    {
        return c * ( ( t = t / d - 1 ) * t * t + 1 ) + b;
    }

    /// <summary>
    /// Easing equation function for a cubic (t^3) easing in: 
    /// accelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double CubicEaseIn( double t, double b, double c, double d )
    {
        return c * ( t /= d ) * t * t + b;
    }

    /// <summary>
    /// Easing equation function for a cubic (t^3) easing in/out: 
    /// acceleration until halfway, then deceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double CubicEaseInOut( double t, double b, double c, double d )
    {
        if ( ( t /= d / 2 ) < 1 )
            return c / 2 * t * t * t + b;

        return c / 2 * ( ( t -= 2 ) * t * t + 2 ) + b;
    }

    /// <summary>
    /// Easing equation function for a cubic (t^3) easing out/in: 
    /// deceleration until halfway, then acceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double CubicEaseOutIn( double t, double b, double c, double d )
    {
        if ( t < d / 2 )
            return CubicEaseOut( t * 2, b, c / 2, d );

        return CubicEaseIn( ( t * 2 ) - d, b + c / 2, c / 2, d );
    }

    /// <summary>
    /// Easing equation function for a quartic (t^4) easing out: 
    /// decelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double QuartEaseOut( double t, double b, double c, double d )
    {
        return -c * ( ( t = t / d - 1 ) * t * t * t - 1 ) + b;
    }

    /// <summary>
    /// Easing equation function for a quartic (t^4) easing in: 
    /// accelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double QuartEaseIn( double t, double b, double c, double d )
    {
        return c * ( t /= d ) * t * t * t + b;
    }

    /// <summary>
    /// Easing equation function for a quartic (t^4) easing in/out: 
    /// acceleration until halfway, then deceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double QuartEaseInOut( double t, double b, double c, double d )
    {
        if ( ( t /= d / 2 ) < 1 )
            return c / 2 * t * t * t * t + b;

        return -c / 2 * ( ( t -= 2 ) * t * t * t - 2 ) + b;
    }

    /// <summary>
    /// Easing equation function for a quartic (t^4) easing out/in: 
    /// deceleration until halfway, then acceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double QuartEaseOutIn( double t, double b, double c, double d )
    {
        if ( t < d / 2 )
            return QuartEaseOut( t * 2, b, c / 2, d );

        return QuartEaseIn( ( t * 2 ) - d, b + c / 2, c / 2, d );
    }
    /// <summary>
    /// Easing equation function for a quintic (t^5) easing out: 
    /// decelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double QuintEaseOut( double t, double b, double c, double d )
    {
        return c * ( ( t = t / d - 1 ) * t * t * t * t + 1 ) + b;
    }

    /// <summary>
    /// Easing equation function for a quintic (t^5) easing in: 
    /// accelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double QuintEaseIn( double t, double b, double c, double d )
    {
        return c * ( t /= d ) * t * t * t * t + b;
    }

    /// <summary>
    /// Easing equation function for a quintic (t^5) easing in/out: 
    /// acceleration until halfway, then deceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double QuintEaseInOut( double t, double b, double c, double d )
    {
        if ( ( t /= d / 2 ) < 1 )
            return c / 2 * t * t * t * t * t + b;
        return c / 2 * ( ( t -= 2 ) * t * t * t * t + 2 ) + b;
    }

    /// <summary>
    /// Easing equation function for a quintic (t^5) easing in/out: 
    /// acceleration until halfway, then deceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double QuintEaseOutIn( double t, double b, double c, double d )
    {
        if ( t < d / 2 )
            return QuintEaseOut( t * 2, b, c / 2, d );
        return QuintEaseIn( ( t * 2 ) - d, b + c / 2, c / 2, d );
    }

    /// <summary>
    /// Easing equation function for an elastic (exponentially decaying sine wave) easing out: 
    /// decelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double ElasticEaseOut( double t, double b, double c, double d )
    {
        if ( ( t /= d ) == 1 )
            return b + c;

        double p = d * .3;
        double s = p / 4;

        return ( c * Math.Pow( 2, -10 * t ) * Math.Sin( ( t * d - s ) * ( 2 * Math.PI ) / p ) + c + b );
    }

    /// <summary>
    /// Easing equation function for an elastic (exponentially decaying sine wave) easing in: 
    /// accelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double ElasticEaseIn( double t, double b, double c, double d )
    {
        if ( ( t /= d ) == 1 )
            return b + c;

        double p = d * .3;
        double s = p / 4;

        return -( c * Math.Pow( 2, 10 * ( t -= 1 ) ) * Math.Sin( ( t * d - s ) * ( 2 * Math.PI ) / p ) ) + b;
    }

    /// <summary>
    /// Easing equation function for an elastic (exponentially decaying sine wave) easing in/out: 
    /// acceleration until halfway, then deceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double ElasticEaseInOut( double t, double b, double c, double d )
    {
        if ( ( t /= d / 2 ) == 2 )
            return b + c;

        double p = d * ( .3 * 1.5 );
        double s = p / 4;

        if ( t < 1 )
            return -.5 * ( c * Math.Pow( 2, 10 * ( t -= 1 ) ) * Math.Sin( ( t * d - s ) * ( 2 * Math.PI ) / p ) ) + b;
        return c * Math.Pow( 2, -10 * ( t -= 1 ) ) * Math.Sin( ( t * d - s ) * ( 2 * Math.PI ) / p ) * .5 + c + b;
    }

    /// <summary>
    /// Easing equation function for an elastic (exponentially decaying sine wave) easing out/in: 
    /// deceleration until halfway, then acceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double ElasticEaseOutIn( double t, double b, double c, double d )
    {
        if ( t < d / 2 )
            return ElasticEaseOut( t * 2, b, c / 2, d );
        return ElasticEaseIn( ( t * 2 ) - d, b + c / 2, c / 2, d );
    }
	
	

    /// <summary>
    /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing out: 
    /// decelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double BounceEaseOut( double t, double b, double c, double d )
    {
        if ( ( t /= d ) < ( 1 / 2.75 ) )
            return c * ( 7.5625 * t * t ) + b;
        else if ( t < ( 2 / 2.75 ) )
            return c * ( 7.5625 * ( t -= ( 1.5 / 2.75 ) ) * t + .75 ) + b;
        else if ( t < ( 2.5 / 2.75 ) )
            return c * ( 7.5625 * ( t -= ( 2.25 / 2.75 ) ) * t + .9375 ) + b;
        else
            return c * ( 7.5625 * ( t -= ( 2.625 / 2.75 ) ) * t + .984375 ) + b;
    }

    /// <summary>
    /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing in: 
    /// accelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double BounceEaseIn( double t, double b, double c, double d )
    {
        return c - BounceEaseOut( d - t, 0, c, d ) + b;
    }

    /// <summary>
    /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing in/out: 
    /// acceleration until halfway, then deceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double BounceEaseInOut( double t, double b, double c, double d )
    {
        if ( t < d / 2 )
            return BounceEaseIn( t * 2, 0, c, d ) * .5 + b;
        else
            return BounceEaseOut( t * 2 - d, 0, c, d ) * .5 + c * .5 + b;
    }

    /// <summary>
    /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing out/in: 
    /// deceleration until halfway, then acceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double BounceEaseOutIn( double t, double b, double c, double d )
    {
        if ( t < d / 2 )
            return BounceEaseOut( t * 2, b, c / 2, d );
        return BounceEaseIn( ( t * 2 ) - d, b + c / 2, c / 2, d );
    }

    /// <summary>
    /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing out: 
    /// decelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double BackEaseOut( double t, double b, double c, double d )
    {
        return c * ( ( t = t / d - 1 ) * t * ( ( 1.70158 + 1 ) * t + 1.70158 ) + 1 ) + b;
    }

    /// <summary>
    /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing in: 
    /// accelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double BackEaseIn( double t, double b, double c, double d )
    {
        return c * ( t /= d ) * t * ( ( 1.70158 + 1 ) * t - 1.70158 ) + b;
    }

    /// <summary>
    /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing in/out: 
    /// acceleration until halfway, then deceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double BackEaseInOut( double t, double b, double c, double d )
    {
        double s = 1.70158;
        if ( ( t /= d / 2 ) < 1 )
            return c / 2 * ( t * t * ( ( ( s *= ( 1.525 ) ) + 1 ) * t - s ) ) + b;
        return c / 2 * ( ( t -= 2 ) * t * ( ( ( s *= ( 1.525 ) ) + 1 ) * t + s ) + 2 ) + b;
    }

    /// <summary>
    /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing out/in: 
    /// deceleration until halfway, then acceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final Value Difference.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    private static double BackEaseOutIn( double t, double b, double c, double d )
    {
        if ( t < d / 2 )
            return BackEaseOut( t * 2, b, c / 2, d );
        return BackEaseIn( ( t * 2 ) - d, b + c / 2, c / 2, d );
    }
	
	private static double Curve(double t, double b, double c, double d, double fOffset)
	{
		t /= d;
		double fCurveOffset = (1 - t)*t*4*fOffset;
		return Linear(t,b,c,d) + fCurveOffset;
	}

}