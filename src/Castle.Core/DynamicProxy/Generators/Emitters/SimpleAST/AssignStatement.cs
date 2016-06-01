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

//using SaveTrees.Logging;

namespace Castle.DynamicProxy.Generators.Emitters.SimpleAST
{
	using System.Reflection.Emit;

	public class AssignStatement : Statement
	{
		private readonly Expression expression;
		private readonly Reference target;

		public AssignStatement(Reference target, Expression expression)
		{
			this.target = target;
			this.expression = expression;
		}

		public override void Emit(IMemberEmitter member, ILGenerator gen)
		{
            //Log.CurrentLogger.Debug()("-START : Emit Assign");
            //Log.CurrentLogger.Debug()("-- {@Member}", member.Member);
            //Log.CurrentLogger.Debug()("-- {@ReturnType}", member.ReturnType);

            //Log.CurrentLogger.Debug()("--START : EmitLoadOwnerAndReference");
            ArgumentsUtil.EmitLoadOwnerAndReference(target.OwnerReference, gen);
            //Log.CurrentLogger.Debug()("--FINISH: EmitLoadOwnerAndReference");

            //Log.CurrentLogger.Debug()("--START : Emit");
            expression.Emit(member, gen);
            //Log.CurrentLogger.Debug()("--FINISH: Emit");

            //Log.CurrentLogger.Debug()("--START : StoreReference");

		    if (member.Member.Name == "ChangeProxyTarget")
		    {
                //Log.CurrentLogger.Debug()("target: {Type}", target.GetType().Name);
                //Log.CurrentLogger.Debug()(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> HERE");
                target.StoreReference(gen);
            }
            else
		    {
                target.StoreReference(gen);
            }
            //Log.CurrentLogger.Debug()("--FINISH: StoreReference");
        }
    }
}