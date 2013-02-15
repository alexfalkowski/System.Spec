require 'albacore'

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
  FileUtils.rm_rf(ARTIFACTS_PATH)
  build.solution = 'System.Spec.sln'
  build.properties = { :configuration => :Release, :OutputPath => ARTIFACTS_PATH }
  build.targets :Rebuild
  build.verbosity = 'quiet'
end

desc 'Run the specs'
nunit :specs => :build do |nunit|
  nunit.command = 'tools/nunit-console'
  nunit.options '-noshadow'
  nunit.assemblies 'artifacts/System.Spec.Specs.dll'
end

desc 'Version the product'
task :assembly_info => [:spec_assembly_info, :spec_console_assembly_info ]

desc 'Version the System.Spec assembly'
assemblyinfo :spec_assembly_info do |asm|
  asm.version = VERSION
  asm.company_name = 'alex.falkowski'
  asm.product_name = 'System.Spec'
  asm.title = 'System.Spec'
  asm.description = 'System.Spec is testing tool for the C# programming language.'
  asm.copyright = Time.now.strftime('%Y')
  asm.output_file = 'System.Spec/AssemblyInfo.cs'
end

desc 'Version the System.Spec console'
assemblyinfo :spec_console_assembly_info do |asm|
  asm.version = VERSION
  asm.company_name = 'alex.falkowski'
  asm.product_name = 'System.Spec.Console'
  asm.title = 'System.Spec.Console'
  asm.description = 'System.Spec is testing tool for the C# programming language.'
  asm.copyright = Time.now.strftime('%Y')
  asm.output_file = 'System.Spec.Console/AssemblyInfo.cs'
end

task :create_merge_folder do
  merged_folder = 'artifacts/merged'
  FileUtils.rm_rf(merged_folder)
  FileUtils.mkdir(merged_folder)
end

desc 'Merge the System.Spec assembly'
exec :merge_spec => :create_merge_folder do |command|
  assemblies = %w(artifacts/System.Spec.dll artifacts/FluentAssertions.dll artifacts/NSubstitute.dll artifacts/nunit.framework.dll)
  command.command = 'tools/ilrepack'
  command.parameters "/target:library /targetplatform:v4 /out:artifacts/merged/System.Spec.dll #{assemblies.join(' ')}"
end

desc 'Merge the System.Spec console'
exec :merge_spec_console => :create_merge_folder do |command|
  assemblies = %w(artifacts/spec.exe artifacts/System.Spec.dll artifacts/FluentAssertions.dll artifacts/NSubstitute.dll artifacts/nunit.framework.dll artifacts/PowerArgs.dll)
  command.command = 'tools/ilrepack'
  command.parameters "/target:winexe /targetplatform:v4 /out:artifacts/merged/spec.exe #{assemblies.join(' ')}"
end

desc 'Merge the the product'
task :merge => [:merge_spec, :merge_spec_console]

desc 'Create the nuspec'
nuspec :nuget_spec do |nuspec|
  nuspec.id = 'System.Spec'
  nuspec.version = VERSION
  nuspec.authors = 'alex.falkowski'
  nuspec.description = 'System.Spec'
  nuspec.title = 'System.Spec'
  nuspec.language = 'en-AU'
  nuspec.projectUrl = 'https://github.com/alexfalkowski/System.Spec'
  nuspec.iconUrl = 'http://2.bp.blogspot.com/-u9nKVHQrC9E/T8g6ecVm-tI/AAAAAAAAA9Q/Sn9SDRcZyyc/s1600/RSpec_logo-07.PNG'
  nuspec.working_directory = 'artifacts'
  nuspec.output_file = 'System.Spec.nuspec'
  nuspec.file 'merged/System.Spec.dll', 'lib/net40'
  nuspec.file 'merged/spec.exe', 'tools'
  nuspec.file 'merged/spec.exe.config', 'tools'
  nuspec.file 'spec.sh', 'tools'
end

desc 'Create the nuget package'
nugetpack :nuget_pack => :nuget_spec do |nuget|
  nuget.command = 'tools/nuget'
  nuget.nuspec = 'artifacts/System.Spec.nuspec'
  nuget.output = 'artifacts'
end

desc 'Push the nuget package'
nugetpush :nuget_push => :nuget_pack do |nuget|
  nuget.command = 'tools/nuget'
  nuget.apikey = '30ab3856-f684-4d0e-9e5e-b8282cdf6fa7'
  nuget.package = "artifacts/System.Spec.#{VERSION}.nupkg"
end