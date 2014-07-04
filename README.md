###Cross-Platform Service Test

(or: I just heard about TopShelf today and wanted to try it out)

This repository contains a C# project called "TopShelfTest." It is a [NancyFX](http://nancyfx.org/) web application that uses [TopShelf](http://topshelf-project.com/) to provide service configuration.

It is meant to explore C# cross-platform compatibility between .NET and Mono (Linux, specifically) for simple web services, and the feasibility of targeting for both platforms simultaneously. Both from a development and packaging/deployment perspective.

The idea is that, if it becomes radically simpler (or at least well-known how simple it is) to target both platforms right out of the gate, more developers will consider C# for their projects and maybe contribute to the community.

####Development

C# is already pretty well cross-platform, as Mono has made great strides toward feature-parity recently. But recent versions aren't usually found in major distro repositories. To tackle this, I provide a `vagrant` box that uses a puppet manifest to install the latest version of Mono from a PPA.

Another minor challenge is the difference in workflows between Windows and Linux developers. I use `rake` to provide a couple of tasks that will execute the correct commands based on environment. For example, choosing xbuild vs msbuild, or pulling packages from `nuget` without having an IDE that provides automatic nuget restore.

####Packaging

The final challenge is packaging for distribution/deployment. When this task is executed on Linux, it generates a `.deb` and `.rpm` package (using [fpm](https://github.com/jordansissel/fpm)). When it is executed on Windows, it uses `WiX` to generate an `.msi`.  It's entirely possible that Linux can package msis through Wine or something or Windows can package debs or rpms, which is something I plan to try adding.

### More Details

As mentioned above, this is a NancyFX application. Just a simple self-hosted application. It uses TopShelf to become a Windows service (see control commands [here](http://docs.topshelf-project.com/en/latest/overview/commandline.html)). It also uses TopShelf.Linux to become a Unix daemon. TopShelf.Linux isn't quite feature-complete yet, but having the Linux package install an Upstart script provides most daemon-like benefits. It's just another way I'm trying to get a 1-to-1 between Windows and Linux.

### Usage Notes

You'll need ruby (with [bundler](http://bundler.io/)) and [vagrant](http://www.vagrantup.com/) installed if you don't already have them. Then execute the `bundle` command to fetch the rest of the needed gems. Note that installing `fpm` on Windows will probably fail, which is okay. I'll assume .NET is a given.

If you're fetching the nuget packages from inside the vagrant box the first time, it may say it is unable to fetch any packages. Simply import the correct SSL certs with the commands [here](http://stackoverflow.com/a/16589218) and it will work.

You need to download the [WiX binaries package](http://wix.codeplex.com/releases/view/115492) and extract it to the `tools\wix` directory before you can bundle an MSI.

### TODO

* Ruby
    * Make the ruby less "just run this command line." Can probably use some plumbing from Albacore
* Vagrant
	* Swap out the vagrant box to one that actually comes with puppet installed
	* Use librarian-puppet or something instead of shell provision in the vagrantfile
* Packaging
	* See if I can make Linux packages from Windows or vice versa
* Misc
	* Quick blog post about this? 
	* Or at least make the code a little bit more accessibile by pointing out the interesting parts here or on the wiki.
	* Make it so I don't need those Usage Notes above
	* Do something actually interesting with this.