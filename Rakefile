require 'albacore'
require 'albacore/exec'

CURRENT_PATH = File.expand_path(File.dirname(__FILE__))
VERSION = '1.0.0'
ARTIFACTS_PATH = File.join(CURRENT_PATH, 'artifacts')

desc 'Get all the referenced packages'
exec :packages do |command|
  FileUtils.rm_rf('packages')
  command.command = 'tools/nuget'
  command.parameters 'install packages.config -o packages'
end

desc 'Build the solution'
xbuild :build => :assembly_info do |build|
  build.solution = 'NSpec.sln'
  build.properties = { :configuration => :Release, :OutputPath => ARTIFACTS_PATH }
  build.targets :Rebuild
  build.verbosity = 'quiet'
end

desc 'Run the specs'
nunit :specs => :build do |nunit|
  nunit.command = 'tools/nunit-console'
  nunit.options '-noshadow'
  nunit.assemblies 'artifacts/NSpec.Specs.dll'
end

desc 'Version the product'
task :assembly_info => [:nspec_assembly_info, :nspec_console_assembly_info ]

desc 'Version the NSpec assembly'
assemblyinfo :nspec_assembly_info do |asm|
  asm.version = VERSION
  asm.company_name = 'alex.falkowski'
  asm.product_name = 'NSpec'
  asm.title = 'NSpec'
  asm.description = 'NSpec is testing tool for the C# programming language.'
  asm.copyright = Time.now.strftime('%Y')
  asm.output_file = 'NSpec/AssemblyInfo.cs'
end

desc 'Version the NSpec console'
assemblyinfo :nspec_console_assembly_info do |asm|
  asm.version = VERSION
  asm.company_name = 'alex.falkowski'
  asm.product_name = 'NSpec.Console'
  asm.title = 'NSpec.Console'
  asm.description = 'NSpec is testing tool for the C# programming language.'
  asm.copyright = Time.now.strftime('%Y')
  asm.output_file = 'NSpec.Console/AssemblyInfo.cs'
end

desc 'Merge the NSpec assembly'
exec :merge_nspec do |command|
  merged_folder = 'artifacts/merged'
  FileUtils.mkdir(merged_folder) unless File.directory?(merged_folder)
  assemblies = %w(artifacts/FluentAssertions.dll artifacts/NSubstitute.dll artifacts/nunit.framework.dll)
  command.command = 'tools/ilrepack'
  command.parameters "/out:artifacts/merged/NSpec.dll #{assemblies.join(' ')}"
end

desc 'Create the nuspec'
nuspec :nuget_spec do |nuspec|
  nuspec.id = 'NSpec'
  nuspec.version = VERSION
  nuspec.authors = 'alex.falkowski'
  nuspec.description = 'NSpec'
  nuspec.title = 'NSpec'
  nuspec.language = 'en-AU'
  nuspec.projectUrl = 'https://github.com/alexfalkowski/NSpec'
  nuspec.iconUrl = 'http://2.bp.blogspot.com/-u9nKVHQrC9E/T8g6ecVm-tI/AAAAAAAAA9Q/Sn9SDRcZyyc/s1600/RSpec_logo-07.PNG'
  nuspec.working_directory = 'artifacts'
  nuspec.output_file = 'NSpec.nuspec'
  nuspec.file 'NSpec.dll', 'lib/net40'
end

desc 'Create the nuget package'
nugetpack :nuget_pack => :nuget_spec do |nuget|
  nuget.command = 'tools/nuget'
  nuget.nuspec = 'artifacts/NSpec.nuspec'
  nuget.output = 'artifacts'
end

desc 'Push the nuget package'
nugetpush :nuget_push => :nuget_pack do |nuget|
  nuget.command = 'tools/nuget'
  nuget.apikey = '30ab3856-f684-4d0e-9e5e-b8282cdf6fa7'
  nuget.package = "artifacts/NSpec.#{VERSION}.nupkg"
end