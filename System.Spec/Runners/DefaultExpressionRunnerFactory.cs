// Author:
//       alex.falkowski <alexrfalkowski@gmail.com>
//
//  Copyright (c) 2013 alex.falkowski
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace System.Spec.Runners
{
    using Monad.Maybe;

    public class DefaultExpressionRunnerFactory : IExpressionRunnerFactory
    {
        public IExpressionRunner CreateExpressionRunner(bool dryRun)
        {
            return new DefaultExpressionRunner(CreateActionStrategy(dryRun));
        }

        private static IOption<IActionStratergy> CreateActionStrategy(bool dryRun)
        {
            return dryRun
                       ? Option.None<IActionStratergy>()
                       : new DefaultActionStratergy().SomeOrNone<IActionStratergy>();
        }
    }
}