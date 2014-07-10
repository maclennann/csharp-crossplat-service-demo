###Cross-Platform Service Test

![Build Status](https://travis-ci.org/maclennann/csharp-crossplat-service-demo.svg)

[Live Version (hosted on Heroku)](http://csharpservice.herokuapp.com/)

[Blog post with details on the project (sans Travis/Heroku)](http://blog.normmaclennan.com/cross-platform-web-services-in-c-with-nancyfx-and-topshelf/)

(or: I just heard about TopShelf today and wanted to try it out)

This repository contains a C# project called "TopShelfTest." It is a [NancyFX](http://nancyfx.org/) web application that uses [TopShelf](http://topshelf-project.com/) to provide service configuration. In short, it provides a basic, minimalistic, cross-platform C# web service.

It is meant to explore C# cross-platform compatibility between .NET and Mono (Linux, specifically) for simple web services, and the feasibility of targeting for both platforms simultaneously. Both from a development and packaging/deployment perspective.

The idea is that, if it becomes radically simpler (or at least well-known how simple it is) to target both platforms right out of the gate, more developers will consider C# for their projects and maybe contribute to the community.

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
