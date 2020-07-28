using System;

namespace Netsoft.Versioning
{
    public class MyVersion : IEquatable<MyVersion>
    {
        private readonly int _major;
        private readonly int _minor;
        private int _patch;
        private int _build;
        private string _tag;

        public MyVersion(int major, int minor)
        {
            _major = major;
            _minor = minor;
        }

        public MyVersion WithPatchVersion(int patch)
        {
            if (patch < 0)
            {
                patch = 0;
            }
            _patch = patch;
            return this;
        }

        public MyVersion WithBuildVersion(int build)
        {
            if (build < 0)
            {
                build = 0;
            }
            _build = build;
            return this;
        }

        public MyVersion WithTagName(string tag)
        {
            if(tag == "")
            {
                tag = null;
            }
            _tag = tag;
            return this;
        }

        public MyVersion UpMajorVersion()
        {
            return new MyVersion(_major + 1, 0)
                .WithPatchVersion(0)
                .WithBuildVersion(0)
                .WithTagName(_tag);
        }
        public MyVersion UpMinorVersion()
        {
            return new MyVersion(_major, _minor + 1)
                .WithPatchVersion(0)
                .WithBuildVersion(0)
                .WithTagName(_tag);
        }
        public MyVersion UpPatchVersion()
        {
            return new MyVersion(_major, _minor)
                .WithPatchVersion(_patch + 1)
                .WithBuildVersion(0)
                .WithTagName(_tag);
        }

        public MyVersion UpBuildVersion()
        {
            return new MyVersion(_major, _minor)
                .WithPatchVersion(_patch)
                .WithBuildVersion(_build + 1)
                .WithTagName(_tag);
        }

        public bool Equals(MyVersion other)
        {
            if (other == null)
            {
                return false;
            }

            return
                _major == other._major &&
                _minor == other._minor &&
                _patch == other._patch &&
                _build == other._build &&
                _tag == other._tag;
        }

        public override string ToString()
        {
            return AppendTag($"{_major}.{_minor}.{_patch}.{_build}");
        }

        public string Format()
        {
            if (_build != 0)
            {
                return AppendTag($"{_major}.{_minor}.{_patch}.{_build}");
            }
            if (_patch != 0)
            {
                return AppendTag($"{_major}.{_minor}.{_patch}");
            }
            return AppendTag($"{_major}.{_minor}");
        }

        private string AppendTag(string value) 
        {
            return string.IsNullOrEmpty(_tag) 
            ? value
            : $"{value}-${_tag}";
        }
    }
}
