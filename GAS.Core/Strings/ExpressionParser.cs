using System;
using System.Collections.Generic;
using System.Text;
namespace GAS.Core.Strings
{
	public static class ExpressionParser
	{
		/// <summary>
		/// Generate random string
		/// syntax:
		/// {I:type:_from:to} - integer
		/// \{I:[DH]:[0-9]+:[0-9]+\}
		/// type
		/// D:dec
		/// H- 0x....
		/// Example:
		/// {I:D:0:1000}
		/// Result example
		/// 384
		/// {C:_from:to} -character
		/// \{C:[0-9]+:[0-9]+\}
		/// Example
		/// {C:1:65535}
		/// Result example
		/// Ё
		/// {S:type:min_length:max_length} -string
		/// \{S:[DHLaRSAU]:[0-9]+:[0-9]+\}
		/// type
		/// D, //0-9
		/// H, //0-f
		/// L, //a-Z
		/// a, //a-z
		/// R, //*
		/// S, //0-Z
		/// A, //A-Z
		/// U //full UTF-8
		/// Example:
		/// {S:a:3:1000}
		/// Result example
		/// gfdfyhtueyrstgdfggfr
		/// {R:{expressions}:min_count:_max_count} - mutiple generator invocation
		/// \{R:\{.*\}:[0-9]+:[0-9]+\}
		/// //2+ level expressions are not supported yet
		/// Example
		/// {R:{{S:L:1:5}={S:U:1:50}&}:1:5}
		/// Result example
		/// werf=%A1%B3&tjy=%5F%9C%2D%42%A1%B3&ertg=%39%7E%E8%B2&
		/// 
		/// 
		/// \{#[ICS](:[a-zA-Z])?:[0-9]+:[0-9]+#\}
		/// Warning! string will NOT be validated
		/// </summary>
		/// <param name="_input"></param>
		/// <returns></returns>
		public static unsafe FormattedStringGenerator Parse(string str) {
			int len = 0;
			fixed ( char* _p = str ) {
				char* p = _p;
				return Parse(ref p, out len, str.Length, new ASCIIEncoding(), new Random());
			}
		}
		public static unsafe RepeatExpression ParseRepeatE(ref char* _from, out int _outcount, int _maxcount, Random _rnd = null, ASCIIEncoding _enc = null) {
			if ( _enc == null )
				_enc = new ASCIIEncoding();
			if ( _rnd == null )
				_rnd = new Random();
			_from += 3;
			RepeatExpression exp = new RepeatExpression();
			exp.Expressions = Parse(ref _from, out _outcount, _maxcount - 3, _enc, _rnd).Expressions;
			_from += 3;
			_outcount += 6;
			int __cnt = 0;
			__cnt = Functions.FindChar(_from, (char*)( _from + _maxcount - _outcount ), ':');
			exp.Min = Functions.QIntParse(_from, __cnt);
			_from += __cnt + 1;
			__cnt = Functions.FindChar(_from, (char*)( _from + _maxcount - _outcount ), '}');
			exp.Max = Functions.QIntParse(_from, __cnt);
			_from += __cnt;
			return exp;
		}
		/// <summary>
		/// parse IntExpression _from string
		/// pointer will point to closing } of expression
		/// </summary>
		/// <param name="_from">pointer to 1st char after opening {</param>
		/// <param name="_outcount">returned value 4 read character _count </param>
		/// <param name="_rnd">randomizer</param>
		/// <returns>parsed expression</returns>
		//works
		public static unsafe IntExpression ParseIntE(ref char* _from, out int _outcount, int _max_count, Random _rnd = null) {
			/*
			* TODO: add string validation
			*/
			#region Variables
			if ( _rnd == null )
				_rnd = new Random();
			int __cnt = 0;
			char* __end = _from + _max_count;
			_outcount = 0;
			IntExpression exp = new IntExpression();
			#endregion
			#region Parse Format
			switch ( *( _from += 2 ) )//skip expression type+separator
			{
				case 'D':
				case 'd':
					exp.Format = NumberFormat.Decimal;
					break;
				case 'H':
				case 'h':
					exp.Format = NumberFormat.Hex;
					break;
				default: break;
			}
			#endregion
			_from += 2;//skip format+separator
			_outcount += 4;//total move
			#region Parse Min
			//get min value length
			__cnt = Functions.FindChar(_from, __end, ':');
			//parse min length
			exp.Min = Functions.QIntParse(_from, __cnt);
			_from += __cnt + 1;//skip separator
			_outcount += __cnt + 1;//add skip 4 min
			#endregion
			#region Parse Max
			//same for max
			__cnt = Functions.FindChar(_from, __end, '}');
			exp.Max = Functions.QIntParse(_from, __cnt);
			_from += __cnt;
			_outcount += __cnt;
			//skip closing bracket
			//_from++;
			#endregion
			return exp;
		}
		/// <summary>
		/// Parses string as ExpressionTree
		/// </summary>
		/// <param name="_from">pointer to __start parsing</param>
		/// <param name="_outcount">__output to save move __offset</param>
		/// <param name="_enc">encoding instanse for generated expressions</param>
		/// <param name="_rnd">randomizer instanse for generated expressions</param>
		/// <param name="_max_count">max string parse length</param>
		/// <returns>expression tree</returns>
		//works
		public unsafe static FormattedStringGenerator Parse(ref char* _from, out int _outcount, int _max_count, ASCIIEncoding _enc = null, Random _rnd = null) {
			#region Variables
			List<IExpression> __exprs = new List<IExpression>();
			char* __start = _from,
			 __end = _from + _max_count;
			int __cnt = 0;
			if ( _rnd == null )
				_rnd = new Random();
			if ( _enc == null )
				_enc = new ASCIIEncoding();
			_outcount = 0;
			#endregion
			#region Parse
			while ( _from < __end ) {
				if ( *_from == '}' ) break;
				if ( *_from == '{' ) {
					#region Add prev string
					if ( --_from >= __start ) {
						__exprs.Add(new StaticASCIIStringExpression(new string(__start, 0, (int)( _from + 1 - __start )), _enc));
					}
					_from++;
					#endregion
					__exprs.Add(ExpresionSelect(ref _from, out __cnt, (int)( __end - _from ), _rnd, _enc));
					_outcount += __cnt;
					__start = _from + 1;
				}
				_from++;
				_outcount++;
			}
			#endregion
			#region Ending string
			if ( --_from >= __start ) {
				__exprs.Add(new StaticASCIIStringExpression(new string(__start, 0, (int)( _from + 1 - __start )), _enc));
				__start = _from;
			}
			_from++;
			#endregion
			return new FormattedStringGenerator() { Expressions = __exprs.ToArray() };
		}
		public static unsafe StringExpression ParseStringE(ref char* _from, out int _outcount, int _max_count, Random _rnd = null) {
			/*
			* TODO: add string validation
			*/
			#region Variables
			if ( _rnd == null )
				_rnd = new Random();
			int __cnt = 0;
			char* __end = _from + _max_count;
			_outcount = 0;
			var exp = new StringExpression();
			#endregion
			#region Parse Format
			switch ( *( _from += 2 ) ) {
				case 'D':
					exp.Format = StringFormat.Decimal;
					break;
				case 'H':
					exp.Format = StringFormat.Hexadecimal;
					break;
				case 'L':
					exp.Format = StringFormat.Letters;
					break;
				case 'a':
					exp.Format = StringFormat.LowerCase;
					break;
				case 'R':
					exp.Format = StringFormat.Random;
					break;
				case 'S':
					exp.Format = StringFormat.Std;
					break;
				case 'A':
					exp.Format = StringFormat.UpperCase;
					break;
				case 'U':
					exp.Format = StringFormat.Urlencode;
					break;
				default: throw new FormatException("Bad string format");
			}
			#endregion
			_from += 2;//skip format+separator
			_outcount += 4;//total move
			#region Parse Min
			//get min value length
			__cnt = Functions.FindChar(_from, __end, ':');
			//parse min length
			exp.Min = Functions.QIntParse(_from, __cnt);
			_from += __cnt + 1;//skip separator
			_outcount += __cnt + 1;//add skip 4 min
			#endregion
			#region Parse Max
			//same for max
			__cnt = Functions.FindChar(_from, __end, '}');
			exp.Max = Functions.QIntParse(_from, __cnt);
			_from += __cnt;
			_outcount += __cnt;
			//skip closing bracket
			//_from++;
			#endregion
			return exp;
		}
		public static unsafe CharExpression ParseCharE(ref char* _from, out int _outcount, int _max_count, Random _rnd = null) {
			/*
			* TODO: add string validation
			*/
			#region Variables
			if ( _rnd == null )
				_rnd = new Random();
			int __cnt = 0;
			char* __end = _from + _max_count;
			var exp = new CharExpression();
			#endregion
			_from += 2;//skip format+separator
			_outcount = 2;//total move
			#region Parse Min
			//get min value length
			__cnt = Functions.FindChar(_from, __end, ':');
			//parse min length
			exp.Min = Functions.QIntParse(_from, __cnt);
			_from += __cnt + 1;//skip separator
			_outcount += __cnt + 1;//add skip 4 min
			#endregion
			#region Parse Max
			//same for max
			__cnt = Functions.FindChar(_from, __end, '}');
			exp.Max = Functions.QIntParse(_from, __cnt);
			_from += __cnt;
			_outcount += __cnt;
			//skip closing bracket
			//_from++;
			#endregion
			return exp;
		}
		internal static unsafe IExpression ExpresionSelect(ref char* _from, out int _outcount, int _max_count, Random _rnd = null, ASCIIEncoding _enc = null) {
			if ( _enc == null )
				_enc = new ASCIIEncoding();
			if ( _rnd == null )
				_rnd = new Random();
			_outcount = 0;
			_max_count--;
			IExpression expr;
			#region Parse
			switch ( *++_from ) {
				case 'I':
					expr = ParseIntE(ref _from, out _outcount, _max_count, _rnd);//works
					break;
				case 'C':
					expr = ParseCharE(ref _from, out _outcount, _max_count, _rnd);
					break;
				case 'S':
					expr = ParseStringE(ref _from, out _outcount, _max_count, _rnd);
					break;
				case 'R':
					expr = ParseRepeatE(ref _from, out _outcount, _max_count, _rnd, _enc);//works
					break;
				default:
					throw new FormatException("Not supported expression");
			}
			#endregion
			_outcount++;
			return expr;
		}
	}
}
