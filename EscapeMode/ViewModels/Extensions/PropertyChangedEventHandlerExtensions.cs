using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;

internal static class PropertyChangedEventHandlerExtensions
{
    /// <summary>
    /// イベントを発行する
    /// </summary>
    /// <typeparam name="TResult">プロパティの型</typeparam>
    /// <param name="_this">イベントハンドラ</param>
    /// <param name="propertyName">プロパティ名を表すExpression。() => Nameのように指定する。</param>
    public static void Raise<TResult>(this PropertyChangedEventHandler _this, Expression<Func<TResult>> propertyName)
    {
        // ハンドラに何も登録されていない場合は何もしない
        if (_this == null) return;

        // ラムダ式のBodyを取得する。MemberExpressionじゃなかったら駄目
        if (!(propertyName.Body is MemberExpression memberEx))
            throw new ArgumentException();

        // () => NameのNameの部分の左側に暗黙的に存在しているオブジェクトを取得する式をゲット
        // ConstraintExpressionじゃないと駄目
        if (!(memberEx.Expression is ConstantExpression senderExpression))
            throw new ArgumentException();

        // ○：定数なのでValueプロパティからsender用のインスタンスを得る
        var sender = senderExpression.Value;

        // 下準備が出来たので、イベント発行！！
        _this(sender, new PropertyChangedEventArgs(memberEx.Member.Name));
    }

    /// <summary>
    /// 前と値が違うなら変更してイベントを発行する
    /// </summary>
    /// <typeparam name="TResult">プロパティの型</typeparam>
    /// <param name="_this">イベントハンドラ</param>
    /// <param name="propertyName">プロパティ名を表すExpression。() => Nameのように指定する。</param>
    /// <param name="source">元の値</param>
    /// <param name="value">新しい値</param>
    /// <returns>値の変更有無</returns>
    public static bool RaiseIfSet<TResult>(this PropertyChangedEventHandler _this, Expression<Func<TResult>> propertyName, ref TResult source, TResult value)
    {
        //値が同じだったら何もしない
        if (EqualityComparer<TResult>.Default.Equals(source, value))
            return false;

        source = value;
        //イベント発行
        Raise(_this, propertyName);
        return true;
    }
}