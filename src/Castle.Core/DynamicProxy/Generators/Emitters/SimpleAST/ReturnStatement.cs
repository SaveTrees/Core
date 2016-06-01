// Copyright 2004-2011 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Castle.DynamicProxy.Generators.Emitters.SimpleAST
{
	using System.Reflection.Emit;

	public class ReturnStatement : Statement
	{
		private readonly Expression expression;
		private readonly Reference reference;

		public ReturnStatement()
		{
		}

		public ReturnStatement(Reference reference)
		{
			this.reference = reference;
		}

		public ReturnStatement(Expression expression)
		{
			this.expression = expression;
		}

		public override void Emit(IMemberEmitter member, ILGenerator gen)
		{
            //Log.CurrentLogger.Debug()("-START : Emit Return");
            //Log.CurrentLogger.Debug()("-- {@Member}", member.Member);
            //Log.CurrentLogger.Debug()("-- {@ReturnType}", member.ReturnType);

            if (reference != null)
			{
                //Log.CurrentLogger.Debug()("--START : Emit 1");
                ArgumentsUtil.EmitLoadOwnerAndReference(reference, gen);
                //Log.CurrentLogger.Debug()("--FINISH: Emit 1");
            }
			else if (expression != null)
			{
                //Log.CurrentLogger.Debug()("--START : Emit 2");
                expression.Emit(member, gen);
                //Log.CurrentLogger.Debug()("--FINISH: Emit 2");
            }
			else
			{
                //Log.CurrentLogger.Debug()("--START : Emit 3");
                if (member.ReturnType != typeof(void))
				{
                    //Log.CurrentLogger.Debug()("--START : Emit 4");
                    OpCodeUtil.EmitLoadOpCodeForDefaultValueOfType(gen, member.ReturnType);
                    //Log.CurrentLogger.Debug()("--FINISH: Emit 4");
                }
                //Log.CurrentLogger.Debug()("--FINISH: Emit 3");
            }

            //Log.CurrentLogger.Debug()("--START : Emit 5");
            gen.Emit(OpCodes.Ret);
            //Log.CurrentLogger.Debug()("--FINISH: Emit 5");
            //Log.CurrentLogger.Debug()("-FINISH: Emit");
        }
    }
}