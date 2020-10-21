using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrategyGame.Bll.Extensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Computes the product of the sequence of System.Double values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns> The product of the projected values.</returns>
        public static double Product<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
        {
            return source.Select(selector).Aggregate(1d, (acc, next) => acc * next);
        }

        
        /// <summary>
        /// Computes the product of the sequence of System.Int32 values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns> The product of the projected values.</returns>
        public static int Product<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        {
            return source.Select(selector).Aggregate(1, (acc, next) => acc * next);
        }

        
        /// <summary>
        /// Computes the product of the sequence of System.Float values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns> The product of the projected values.</returns>
        public static float Product<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
        {
            return source.Select(selector).Aggregate(1f, (acc, next) => acc * next);
        }


    }
}
